using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService
{
    public class Constants
    {
        #region Session

        public const string VALID_USER = "validUser";
        public const string IS_ADMIN = "isAdmin";
        public const string USER_ID = "userId";
        public const string USER_TOKEN = "userToken";

        #endregion

        #region DB

        #region Columns

        public const string ID = "id";
        public const string SSN = "ssn";
        public const string FIRST_NAME = "firstname";
        public const string LAST_NAME = "lastname";
        public const string PHONE = "phone";
        public const string DATE_SUB = "date_submitted";

        public const string USERNAME = "username";
        public const string PASSWORD = "password";
        public const string USER_TYPE = "UserType";

        public const string GUID = "GUID";
        public const int GUID_LENGTH = 36;
        public const string LAST_ACCESSED = "lastAccessed";

        #endregion

        #region Tables

        public const string TABLE_APP_DATA = "UserDatabase.dbo.AppData";
        public const string TABLE_USERS = "UserDatabase.dbo.Users";
        public const string TABLE_USER_TOKENS = "UserDatabase.dbo.UserTokens";

        #endregion

        #endregion

        #region Page URLs

        public const string ADMIN_PAGE = "/AdminView.aspx";
        public const string VIEW_PAGE_PARTIAL = "/View.aspx?id=";
        public const string LOGIN_PAGE = "/";

        #endregion


    }
}