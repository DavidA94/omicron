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
        protected void Page_Load(object sender, EventArgs e)
        {
            var service = new OmicronService.OmicronServiceClient();
            var users = service.GetUserData(-1);

            adminTableRow.DataSource = users;
            adminTableRow.DataBind();
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            //Session[Constants.VALID_USER] = false;
            //Session[Constants.ID] = null;
            ///Session[Constants.IS_ADMIN] = false;

            Response.Redirect("/");
        }

        protected void ViewLink_Click(object sender, EventArgs e)
        {
            Session[Constants.ID] = Convert.ToInt32((sender as LinkButton).ID);
            Response.Redirect("/ViewData.aspx");
        }
    }
}