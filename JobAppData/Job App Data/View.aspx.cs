using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebService;
using WebService.DataContracts;

namespace Job_App_Data
{
    public partial class View : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!validSession())
            {
                return;
            }

            int id = Convert.ToInt32(Request.QueryString[Constants.ID]);
            var service = new OmicronService.OmicronServiceClient();
            var userData = service.GetUserData(id, (Guid)Session[Constants.USER_TOKEN])[0];

            Title = userData.FirstName + " " + userData.LastName + " Application";

            // We should only get one.
            placeUserData(userData);
        }

        protected void saveUserDetailsButton_Click(object sender, EventArgs e)
        {

        }

        protected void returnAdminLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("/AdminView.aspx");
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

        private void placeUserData(AppDataContract userData)
        {
            bool canChange = userData.Changeable;

            SSN.Text = userData.SSN;
            setEditability(SSN, "SSN", canChange);

            FirstName.Text = userData.FirstName;
            setEditability(FirstName, "FirstName", canChange);

            LastName.Text = userData.LastName;
            setEditability(LastName, "LastName", canChange);

            Phone.Text = userData.Phone;
            setEditability(Phone, "Phone", canChange);

            DateSubmitted.Text = userData.DateSubmitted.ToShortDateString();
            setEditability(DateSubmitted, "DateSubmitted", canChange);

            bool isAdmin = (bool)Session[Constants.IS_ADMIN];
            returnAdminLink.Visible = isAdmin;
            saveUserDetailsButton.Visible = !isAdmin;
        }

        private void setEditability(TextBox tb, string property, bool enabled)
        {
            tb.Enabled = (enabled && canSet(property));
        }

        private bool canSet(string property)
        {
            var prop = typeof(AppDataContract).GetProperty(property);
            if(prop != null)
            {
                var setter = prop.GetSetMethod();

                if(setter != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}