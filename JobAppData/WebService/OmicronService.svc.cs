using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using WebService.DataContracts;

namespace WebService
{
    /// <summary>
    /// Indicates the type of user interacting with the system
    /// </summary>
    public enum UserType { USER, ADMIN, INVALID }

    public class OmicronService : IOmicronService
    {
        #region Private Variables

        /// <summary>
        /// A connection to the Database.
        /// </summary>
        private SqlConnection DbConnection = null;

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the data of one or more users.
        /// </summary>
        /// <param name="id">The ID of the user to get (below 0 for administrators)</param>
        /// <param name="guid">The token of the user requesting the data</param>
        /// <returns>An array of users' data, or null if an invalid use</returns>
        public AppDataContract[] GetUserData(int id, Guid guid)
        {
            openConnection();

            List<AppDataContract> data = null;
            UserToken user = getUserFromToken(guid);


            if (user != null)
            {
                data = new List<AppDataContract>();
                // Update that the user has made a request.
                updateLastAccessed(guid);

                if (id < 0 && user.Type == UserType.ADMIN)
                {
                    /* SELECT Users.id, ssn, firstname, lastname, phone, date_submitted
                       FROM UserDatabase.dbo.Users
                       JOIN UserDatabase.dbo.AppData
                       ON UserDatabase.dbo.Users.id = UserDatabase.dbo.AppData.id
                       WHERE UserType = @usertype;
                    */
                    string getUserDataCommand = string.Format("SELECT {0}.{1}, {2}, {3}, {4}, {5}, {6} " +
                                                              "FROM {0} " +
                                                              "JOIN {7} " +
                                                              "ON {0}.{1} = {7}.{1} " +
                                                              "WHERE {8} = @usertype",
                                                              Constants.TABLE_USERS, Constants.ID,
                                                              Constants.SSN,
                                                              Constants.FIRST_NAME,
                                                              Constants.LAST_NAME,
                                                              Constants.PHONE,
                                                              Constants.DATE_SUB,
                                                              Constants.TABLE_APP_DATA,
                                                              Constants.USER_TYPE);

                    var getAll = makeCommand(getUserDataCommand, new PreparedData(SqlDbType.Int, (int)UserType.USER));

                    using (SqlDataReader reader = getAll.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data.Add(new AppDataContract((int)reader[Constants.ID],
                                                         (string)reader[Constants.SSN],
                                                         (string)reader[Constants.FIRST_NAME],
                                                         (string)reader[Constants.LAST_NAME],
                                                         (string)reader[Constants.PHONE],
                                                         (DateTime)reader[Constants.DATE_SUB])
                            );
                        }
                    }
                }
                else if (user.ID == id || user.Type == UserType.ADMIN)
                {
                    /*
                    "SELECT ssn, firstname, lastname, phone, date_submitted " +
                                              "FROM UserDatabase.dbo.AppData " +
                                              "WHERE id = @userID"
                    */
                    var getUser = makeCommand(string.Format("SELECT {0}, {1}, {2}, {3}, {4} " +
                                              "FROM {5} " +
                                              "WHERE {6} = @userID",
                                              Constants.SSN,
                                              Constants.FIRST_NAME,
                                              Constants.LAST_NAME,
                                              Constants.PHONE,
                                              Constants.DATE_SUB,
                                              Constants.TABLE_APP_DATA,
                                              Constants.ID),
                    /* ------------------- */ new PreparedData(SqlDbType.Int, id))

                   ;


                    using (SqlDataReader reader = getUser.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            data.Add(new AppDataContract(id,
                                                         (string)reader[Constants.SSN],
                                                         (string)reader[Constants.FIRST_NAME],
                                                         (string)reader[Constants.LAST_NAME],
                                                         (string)reader[Constants.PHONE],
                                                         (DateTime)reader[Constants.DATE_SUB],
                                                         user.Type != UserType.ADMIN)
                            );
                        }
                    }
                }
            }

            closeConnection();

            return data.ToArray();
        }

        /// <summary>
        /// Logs a user out
        /// </summary>
        /// <param name="guid">The token of the user to log out.</param>
        public void logout(Guid guid)
        {
            openConnection();

            makeCommand(string.Format("UPDATE {0} SET {1}=@minTime WHERE {2}=@guid",
                                      Constants.TABLE_USER_TOKENS,
                                      Constants.LAST_ACCESSED,
                                      Constants.GUID),
                        new PreparedData(SqlDbType.DateTime, DateTime.MinValue),
                        new PreparedData(SqlDbType.Char, guid.ToString(), Constants.GUID_LENGTH)).ExecuteNonQuery();

            closeConnection();
        }

        /// <summary>
        /// Changes the users data with the new data.
        /// </summary>
        /// <param name="userData">The updated data</param>
        /// <param name="GUID">The user token to authenticate this action</param>
        /// <returns>True if the user was updated</returns>
        public bool ChangeUserData(AppDataContract userData, Guid GUID)
        {
            UserToken user = getUserFromToken(GUID);

            if (user.Type == UserType.USER && user.ID == userData.ID)
            {
                openConnection();


                makeCommand(string.Format("UPDATE {0} " +
                                      "SET {1}=@firstName, {2}=@lastName, {3}=@phone " +
                                      "WHERE {4}=@userId",
                                      Constants.TABLE_APP_DATA,
                                      Constants.FIRST_NAME,
                                      Constants.LAST_NAME,
                                      Constants.PHONE,
                                      Constants.ID),
                            new PreparedData(SqlDbType.VarChar, userData.FirstName, 50),
                            new PreparedData(SqlDbType.VarChar, userData.LastName, 50),
                            new PreparedData(SqlDbType.Char, userData.Phone, 10),
                            new PreparedData(SqlDbType.Int, userData.ID)
                ).ExecuteNonQuery();

                closeConnection();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if a user is valid based on their credentials
        /// </summary>
        /// <param name="username">The user's username</param>
        /// <param name="password">The user's password</param>
        /// <returns>A new ValidUserContract with a GUID to be used for further requests, or null if an invalid user.</returns>
        public ValidUserContract ValidUser(string username, string password)
        {
            openConnection();
            UserType userType;
            int? userID = null;

            SqlCommand checkUser = makeCommand(string.Format("SELECT * FROM {0} WHERE {1}=@user AND {2}=@pass",
                                                             Constants.TABLE_USERS,
                                                             Constants.USERNAME,
                                                             Constants.PASSWORD),
                                               new PreparedData(SqlDbType.VarChar, username, 15),
                                               new PreparedData(SqlDbType.VarChar, password, 50));

            using (SqlDataReader reader = checkUser.ExecuteReader())
            {
                if (reader.Read())
                {
                    userType = (UserType)reader[Constants.USER_TYPE];
                    userID = (int?)reader[Constants.ID];
                }
                else
                {
                    userType = UserType.INVALID;
                }
            }

            var user = new ValidUserContract(userID, userType);

            if (userType != UserType.INVALID)
            {
                makeNewUser(user, userID);
            }

            closeConnection();
            return user;
        }

        #endregion

        #region Database Helpers

        /// <summary>
        /// Creates a new Prepared SQL query to be run
        /// </summary>
        /// <param name="query">The query to be run</param>
        /// <param name="data">The Prepared Data to be used in conjunction with the query</param>
        /// <returns>A new SQL command ready to be executed.</returns>
        private SqlCommand makeCommand(string query, params PreparedData[] data)
        {
            openConnection();

            if (query.LastOrDefault() != ';')
            {
                query += ";";
            }

            SqlCommand pQuery = new SqlCommand(query, DbConnection);

            var markers = Regex.Matches(query, @"\@\w+");
            if (markers.Count != data.Length)
            {
                throw new ArgumentException("Number of data markers and data do not match");
            }

            for (int i = 0; i < markers.Count; ++i)
            {
                if (data[i].Length > 0)
                {
                    var param = pQuery.Parameters.Add(markers[i].Value, data[i].Type, data[i].Length);
                    param.Value = data[i].Data;
                }
                else
                {
                    pQuery.Parameters.Add(markers[i].Value, data[i].Type).Value = data[i].Data;
                }
            }

            pQuery.Prepare();

            return pQuery;
        }

        #endregion

        #region Database Management

        /// <summary>
        /// Opens a Database connection, if one has not been opened yet.
        /// </summary>
        private void openConnection()
        {
            if (DbConnection == null)
            {
                DbConnection = new SqlConnection("Data Source=(localdb)\\ProjectsV12;Initial Catalog=UserDatabase;Integrated Security=True;Pooling=False;Connect Timeout=3");
                DbConnection.Open();
            }
        }

        /// <summary>
        /// Closes the database connection, if one is open.
        /// </summary>
        private void closeConnection()
        {
            if (DbConnection != null)
            {
                DbConnection.Close();
                DbConnection = null;
            }
        }

        #endregion

        #region User Database Helpers

        /// <summary>
        /// Updates a user so that their session won't timeout
        /// </summary>
        /// <param name="guid">The token of the user</param>
        private void updateLastAccessed(Guid guid)
        {
            openConnection();

            var updateUser = makeCommand(string.Format("UPDATE {0} SET {1}=GETDATE() WHERE {2}=@guid",
                                                       Constants.TABLE_USER_TOKENS,
                                                       Constants.LAST_ACCESSED,
                                                       Constants.GUID),
                                         new PreparedData(SqlDbType.Char, guid.ToString(), Constants.GUID_LENGTH));
        }

        /// <summary>
        /// Creates a new user in the tokens database.
        /// </summary>
        /// <param name="user">The user's data to be used</param>
        /// <param name="userID">The ID of the user</param>
        private void makeNewUser(ValidUserContract user, int? userID)
        {
            openConnection();

            makeCommand(string.Format("INSERT INTO {0} VALUES (@guid, GETDATE(), @type, @id)", Constants.TABLE_USER_TOKENS),
                                      new PreparedData(SqlDbType.Char, user.GUID.ToString(), Constants.GUID_LENGTH),
                                      new PreparedData(SqlDbType.Int, user.UserType),
                                      new PreparedData(SqlDbType.Int, userID)
            ).ExecuteNonQuery();
        }

        /// <summary>
        /// Checks if a user is valid, based on their token (GUID)
        /// </summary>
        /// <param name="guid">The token for the user</param>
        /// <returns>A new UserToken, or null if the user is not found</returns>
        private UserToken getUserFromToken(Guid guid)
        {
            openConnection();

            // Check if this is a valid user
            var getUser = makeCommand(string.Format("SELECT * FROM {0} WHERE {1}=@guid",
                                                    Constants.TABLE_USER_TOKENS,
                                                    Constants.GUID),
                                      new PreparedData(SqlDbType.Char, guid.ToString(), Constants.GUID_LENGTH));

            using (SqlDataReader reader = getUser.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new UserToken((string)reader[Constants.GUID],
                                         (DateTime)reader[Constants.LAST_ACCESSED],
                                         (UserType)reader[Constants.USER_TYPE],
                                         (int?)reader[Constants.USER_ID]);
                }
            }

            return null;
        }

        #endregion
    }

}
