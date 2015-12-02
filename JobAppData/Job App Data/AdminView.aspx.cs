using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebService;

namespace Job_App_Data
{
    public partial class AdminView : System.Web.UI.Page
    {
        /// <summary>
        /// Called when the page loads
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // If we don't have a valid session, then get out of here
            if (!validSession())
            {
                return;
            }

            // Otherwise, open up a link to the web service
            var service = new OmicronService.OmicronServiceClient();

            // Get all the users
            var users = service.GetUserData(-1, (Guid)Session[Constants.USER_TOKEN]);

            // If this is null, then we aren't authorized to be here, so let's leave.
            if(users == null)
            {
                Response.Redirect(Constants.LOGIN_PAGE);
                return;
            }

            // Otherwise, fill up the table with the user's data.
            adminTableRow.DataSource = users;
            adminTableRow.DataBind();
        }

        /// <summary>
        /// Called when the logout button is clicked.
        /// </summary>
        protected void Logout_Click(object sender, EventArgs e)
        {
            // If we don't have a session to being with, then just go to the login page.
            if (!validSession())
            {
                return;
            }

            // Otherwise, tell the web service we are logging out
            (new OmicronService.OmicronServiceClient()).logout((Guid)Session[Constants.USER_TOKEN]);

            // Clear the sesison
            Session[Constants.USER_TOKEN] = null;

            // And go to the login page.
            Response.Redirect(Constants.LOGIN_PAGE);
        }
        
        /// <summary>
        /// Checks if we have a session token.
        /// </summary>
        /// <returns>True if there is a session token</returns>
        private bool validSession()
        {
            // If the user token isn't null
            if (Session[Constants.USER_TOKEN] == null)
            {
                // Set the redirect and return that we don't have a valid token
                Response.Redirect(Constants.LOGIN_PAGE);
                return false;
            }

            // Otherwise return that we do have a token.
            return true;
        }
    }
}