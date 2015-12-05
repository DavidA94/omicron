using System;
using System.ServiceModel;
using WebService.DataContracts;

namespace WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IOmicronService
    {

        /// <summary>
        /// Gets the data of one or more users.
        /// </summary>
        /// <param name="id">The ID of the user to get (below 0 for administrators)</param>
        /// <param name="guid">The token of the user requesting the data</param>
        /// <returns>An array of users' data</returns>
        [OperationContract]
        AppDataContract[] GetUserData(int id, Guid GUID);

        /// <summary>
        /// Logs a user out
        /// </summary>
        /// <param name="guid">The token of the user to log out.</param>
        [OperationContract]
        void logout(Guid GUID);

        /// <summary>
        /// Checks if a user is valid based on their credentials
        /// </summary>
        /// <param name="username">The user's username</param>
        /// <param name="password">The user's password</param>
        /// <returns>A new ValidUserContract with a GUID to be used for further requests, or null if an invalid user.</returns>
        [OperationContract]
        ValidUserContract ValidUser(string username, string password);

        /// <summary>
        /// Changes the users data with the new data.
        /// </summary>
        /// <param name="userData">The updated data</param>
        /// <param name="GUID">The user token to authenticate this action</param>
        /// <returns>True if the user was updated</returns>
        [OperationContract]
        bool ChangeUserData(AppDataContract userData, Guid GUID);
    }
}
