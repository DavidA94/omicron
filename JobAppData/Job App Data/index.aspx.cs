﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebService;

namespace Job_App_Data
{
    public partial class index : System.Web.UI.Page
    {
        /// <summary>
        /// Called when the login button is pressed
        /// </summary>
        protected void LoginData_Authenticate(object sender, AuthenticateEventArgs e)
        {
            // Open a connection to the service
            var service = new OmicronService.OmicronServiceClient();

            WebService.DataContracts.ValidUserContract user;

            // Get the user based of their credentials
            try
            {
                user = service.ValidUser(LoginData.UserName, LoginData.Password);
            }
            catch
            {
                return;
            }

            // If an invalid user, we're done here.
            if(user.UserType == UserType.INVALID)
            {
                return;
            }

            Session[Constants.USER_TOKEN] = user.GUID;
            Session[Constants.IS_ADMIN] = user.UserType == UserType.ADMIN;

            // If they're an admin, then go to the admin view
            if (user.UserType == WebService.UserType.ADMIN)
            {
                Response.Redirect("/AdminView.aspx");
            }
            // Otherwise, if they're a user, go to the user view
            else if(user.UserType == WebService.UserType.USER)
            {
                Response.Redirect("/View.aspx?id=" + user.ID);
            }

            // Otherwise, they'll be given an invalid login.
        }
    }
}