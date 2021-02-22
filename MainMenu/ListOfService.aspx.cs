using MainMenu.DataAccessObject;
using MainMenu.Models;
using MainMenu.SQL_Procedures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace MainMenu
{
    public partial class ListOfService : System.Web.UI.Page
    {
        public List<Service> listOfServices = new List<Service>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillServices();
            }
            listOfServices = SQLProcedures.selectSveServise().ToList();
        }

        private void FillServices()
        {
            listOfServices = new List<Service>();
            listOfServices = SQLProcedures.selectSveServise().ToList();
            GridListofServices.DataSource = listOfServices;
            GridListofServices.DataBind();
        }

        protected void GridListofServices_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridListofServices, "Select$" + e.Row.RowIndex);
            }
        }

        protected void GridListofServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridListofServices.DataSource = SQLProcedures.selectSveServise();
            GridListofServices.DataBind();
            foreach (GridViewRow row in GridListofServices.Rows)
            {
                if (row.RowIndex == GridListofServices.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("Black");
                    row.ForeColor = ColorTranslator.FromHtml("White");
                    InputName.Text = GridListofServices.SelectedRow.Cells[1].Text;
                    DialogResult dialogResult = MessageBox.Show("Do you want to create Bill?", "Create Bill", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Session["ServiceID"] = GridListofServices.SelectedIndex + 1;
                        int vehicleID = (int)Session["VehicleID"];
                        int serviceID = (int)Session["ServiceID"];
                        SQLProcedures.createRacun(vehicleID, serviceID);
                        MessageBox.Show("The bill has been created!", "Done");
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

        protected void Add_Click(object sender, EventArgs e)
        {
            string name = InputName.Text;

            bool exists = false;
            foreach (var item in listOfServices)
            {
                if (InputName.Text.ToString() == item.Naziv_Servisa.ToString())
                {
                    exists = true;
                    break;
                }
            }
            if (exists)
            {
                FillServices();
                LblError.Text = "Sorry, that service already exists in the database!";
                return;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    try
                    {
                        SQLProcedures.createServis(name);
                        FillServices();
                        ClearGrid();
                    }
                    catch (Exception ex)
                    {
                        LblError.Text = $"Exception: {ex.Message}";
                    }
                }
            }
        }

        private void ClearGrid()
        {
            InputName.Text = null;
            LblError.Text = null;
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            if (GridListofServices.SelectedRow == null || GridListofServices.Rows.Count == 0)
            {
                LblError.Text = "Please select a service!";
                FillServices();
            }
            else
            {
                int idService = int.Parse(GridListofServices.SelectedRow.Cells[0].Text);
                string name = InputName.Text;

                if (!string.IsNullOrWhiteSpace(name))
                {
                    try
                    {
                        SQLProcedures.updateServis(idService, name);
                        FillServices();
                        ClearGrid();

                    }
                    catch (Exception ex)
                    {
                        LblError.Text = $"Exception: {ex.Message}";
                    }
                }
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            if (GridListofServices.SelectedRow == null || GridListofServices.Rows.Count == 0)
            {
                LblError.Text = "Please select a service!";
                FillServices();
            }
            else
            {
                int idService = int.Parse(GridListofServices.SelectedRow.Cells[0].Text);
                try
                {
                    SQLProcedures.deleteServis(idService);
                    FillServices();
                    ClearGrid();

                }
                catch (Exception ex)
                {
                    LblError.Text = $"Exception: {ex.Message}";
                }
            }
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AllVehicles.aspx");
        }
    }
}