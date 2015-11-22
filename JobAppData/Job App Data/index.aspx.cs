using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Job_App_Data
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginData_Authenticate(object sender, AuthenticateEventArgs e)
        {
            var service = new OmicronService.OmicronServiceClient();
            WebService.UserType userType = service.ValidUser(LoginData.UserName, LoginData.Password);
            if (userType == WebService.UserType.ADMIN)
            {
                Response.Redirect("/AdminView.aspx");
            }
            else if(userType == WebService.UserType.USER)
            {
                // Go to user page
            }
        }
    }
}