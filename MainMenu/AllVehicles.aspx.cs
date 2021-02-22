using MainMenu.DataAccessObject;
using MainMenu.HTML;
using MainMenu.Models;
using MainMenu.SQL_Procedures;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace MainMenu
{
    public partial class AllVehicles : System.Web.UI.Page
    {
        public List<Vehicles> listOfVehicles = new List<Vehicles>();
        public List<ServiseBill> listOfBills = new List<ServiseBill>();
        public Context db = new Context();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillVehicles();
            }
            listOfVehicles = SQLProcedures.selectSvaVozila().ToList();
        }

        private void FillVehicles()
        {
            listOfVehicles = new List<Vehicles>();
            listOfVehicles = SQLProcedures.selectSvaVozila().ToList();
            GridListofVehicles.DataSource = listOfVehicles;
            GridListofVehicles.DataBind();
        }

        protected void GridListofVehicles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridListofVehicles, "Select$" + e.Row.RowIndex);
            }
        }

        protected void GridListofVehicles_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridListofVehicles.DataSource = SQLProcedures.selectSvaVozila();
            GridListofVehicles.DataBind();
            foreach (GridViewRow row in GridListofVehicles.Rows)
            {
                if (row.RowIndex == GridListofVehicles.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("Black");
                    row.ForeColor = ColorTranslator.FromHtml("White");
                    Session["VehicleID"] = GridListofVehicles.SelectedIndex + 1;
                    DialogResult dialogResult = MessageBox.Show("Do you want to see the list of Services?", "List of Service", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Response.Redirect("~/ListOfService.aspx");
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
            }
        }

        protected void btnCreateHTMLDocument_Click(object sender, EventArgs e)
        {
            var allBills = db.Racuni;
            listOfBills = new List<ServiseBill>();
            Vehicles vehicle = new Vehicles();
            listOfVehicles.ForEach(vh =>
            {
                if (vh.IDVozilo == (int)Session["VehicleID"])
                {
                    vehicle = vh;
                }
            });
            allBills.ToList().ForEach(b =>
            {
                if (b.VoziloID == (int)Session["VehicleID"])
                {
                    listOfBills.Add(b);
                }
            });
            HTMLCreate.createHTML(listOfBills, vehicle);
            FillVehicles();
        }
    }
}