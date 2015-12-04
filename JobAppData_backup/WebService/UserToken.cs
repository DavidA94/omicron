using System;

namespace WebService
{
    public class UserToken
    {
        public UserToken(string guid, DateTime lastAccessed, UserType userType, int? id)
        {
            GUID = guid;
            LastAccessed = lastAccessed;
            Type = userType;
            ID = id;
        }

        /// <summary>
        /// The GUID/Token of the user
        /// </summary>
        public string GUID { get; private set; }

        /// <summary>
        /// The last time this user interfaced with the system
        /// </summary>
        public DateTime LastAccessed { get; private set; }

        /// <summary>
        /// The type of user
        /// </summary>
        public UserType Type { get; private set; }

        /// <summary>
        /// The ID of the user (null if admin)
        /// </summary>
        public int? ID { get; private set; }
    }
}