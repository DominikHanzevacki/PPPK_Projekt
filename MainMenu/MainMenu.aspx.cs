using MainMenu.SQL_Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MainMenu
{
    public partial class MainMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnCRUDDrivers_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CRUD Drivers.aspx");
        }
        protected void btnCRUDWarrants_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CRUD TravelWarrants.aspx");
        }

        protected void btnResetDatabase_Click(object sender, EventArgs e)
        {
            SQLProcedures.resetDatabase();
        }

        protected void btnXMLDatabase_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/XML.aspx");
        }

        protected void btnShowVehicles_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AllVehicles.aspx");
        }
    }
}