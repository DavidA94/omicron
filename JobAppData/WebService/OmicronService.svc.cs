using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;

namespace WebService
{
    public enum UserType { USER, ADMIN, INVALID }

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class OmicronService : IOmicronService
    {
        private SqlConnection DbConnection = null;

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public UserType ValidUser(string username, string password)
        {
            openConnection();
            UserType userType;

            SqlCommand checkUser = makeCommand("SELECT * FROM dbo.Users WHERE username=@user AND password=@pass",
                new PreparedData(SqlDbType.VarChar, username, 15), 
                new PreparedData(SqlDbType.VarChar,  password, 50));

            using(SqlDataReader reader = checkUser.ExecuteReader())
            {
                // var session = HttpContext.Current.Session;

                if (reader.Read())
                {
                    //session[Constants.IS_ADMIN] = ((UserType)reader[Constants.USER_TYPE] == UserType.ADMIN);
                    //session[Constants.USER_ID] = reader[Constants.ID];
                    //session[Constants.VALID_USER] = true;

                    userType = (UserType)reader[Constants.USER_TYPE];
                }
                else
                {
                    // session[Constants.VALID_USER] = false;
                    userType = UserType.INVALID;
                }
            }

            closeConnection();
            return userType;
        }

        [WebMethod(EnableSession = true)]
        public AppDataContract[] GetUserData(int id)
        {
            var session = HttpContext.Current.Session;
            var data = new List<AppDataContract>();

            if(true || (bool)session[Constants.VALID_USER])
            {
                if (id < 0 && true) //(bool)session["isAdmin"])
                {
                    var getAll = makeCommand("SELECT Users.id, ssn, firstname, lastname, phone, date_submitted " +
                                             "FROM UserDatabase.dbo.Users " +
                                             "JOIN UserDatabase.dbo.AppData " +
                                             "ON UserDatabase.dbo.Users.id = UserDatabase.dbo.AppData.id " +
                                             "WHERE UserType = @usertype;",
                    /* ------------------ */ new PreparedData(SqlDbType.Int, (int)UserType.USER));

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
                else if (true || (int)session["userID"] == id)
                {
                    var getUser = makeCommand("SELECT ssn, firstname, lastname, phone, date_submitted " +
                                              "FROM UserDatabase.dbo.Users " +
                                              "WHERE id = @userID",
                    /* ------------------- */ new PreparedData(SqlDbType.Int, id));


                    using (SqlDataReader reader = getUser.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            data.Add(new AppDataContract((int)reader[Constants.ID],
                                                         (string)reader[Constants.SSN],
                                                         (string)reader[Constants.FIRST_NAME],
                                                         (string)reader[Constants.LAST_NAME],
                                                         (string)reader[Constants.PHONE],
                                                         (DateTime)reader[Constants.DATE_SUB],
                                                         true)
                            );
                        }
                    }
                }
            }

            return data.ToArray();
        }

        private void openConnection()
        {
            if(DbConnection == null)
            {
                DbConnection = new SqlConnection("Data Source=(localdb)\\ProjectsV12;Initial Catalog=UserDatabase;Integrated Security=True;Pooling=False;Connect Timeout=3");
                DbConnection.Open();
            }
        }

        private void closeConnection()
        {
            if(DbConnection != null)
            {
                DbConnection.Close();
                DbConnection = null;
            }
        }

        private SqlCommand makeCommand(string query, params PreparedData[] data)
        {
            openConnection();

            if(query.LastOrDefault() != ';')
            {
                query += ";";
            }

            SqlCommand pQuery = new SqlCommand(query, DbConnection);

            var markers = Regex.Matches(query, @"\@\w+");
            if(markers.Count != data.Length)
            {
                throw new ArgumentException("Number of data markers and data do not match");
            }

            for(int i = 0; i < markers.Count; ++i)
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
    }

    public class PreparedData
    {
        public PreparedData(SqlDbType type, object data, int length = -1)
        {
            Type = type;
            Data = data;
            Length = length;
        }
        public SqlDbType Type { get; set; }
        public object Data { get; set; }
        public int Length { get; set; }
    }
}
