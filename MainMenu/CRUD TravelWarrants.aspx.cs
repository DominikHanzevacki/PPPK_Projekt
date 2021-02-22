using MainMenu.Models;
using MainMenu.SQL_Procedures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MainMenu
{
    public partial class CRUD_TravelWarrants : System.Web.UI.Page
    {
        private List<Drivers> listOfDriver;
        private List<Vehicles> listOfVehicles;
        private List<TravelWarrant> listOfTravelWarrants;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                FillTravelWarrants();
            }
            FillLists();
            FillDDLLists();

        }

        private void FillTravelWarrants()
        {
            GridListOfWarrants.DataSource = SQLProcedures.selectSvePutneNaloge();
            GridListOfWarrants.DataBind();
        }

        private void FillLists()
        {
            listOfDriver = SQLProcedures.selectSveVozace().ToList();
            listOfVehicles = SQLProcedures.selectSvaVozila().ToList();
            listOfTravelWarrants = SQLProcedures.selectSvePutneNaloge().ToList();
        }

        private void FillDDLLists()
        {
            if (DdlDrivers.Items.Count.Equals(0))
            {
                foreach (Drivers driver in listOfDriver)
                {
                    DdlDrivers.Items.Add(driver.IDVozac.ToString());
                }
            }
            if (DdlVehicles.Items.Count.Equals(0))
            {
                foreach (Vehicles vehicles in listOfVehicles)
                {
                    DdlVehicles.Items.Add(vehicles.IDVozilo.ToString());
                }
            }
           
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            string startingPlace = Place_of_Departure.Text;
            string endingPlace = Destination_Place.Text;
            int driverID = int.Parse(DdlDrivers.SelectedItem.Text);
            int vehicleID = int.Parse(DdlVehicles.SelectedItem.Text);
            string startingDate = Date_of_issue.Text;
            string endingDate = Delivery_Date.Text;

            bool exists = false;
            foreach (TravelWarrant warrant in listOfTravelWarrants)
            {
                if (driverID == warrant.VozacID && vehicleID == warrant.VoziloID)
                {
                    exists = true;
                    break;
                }
            }
            if (exists)
            {
                FillTravelWarrants();
                LblError.Text = "Sorry, that warrant already exists in the database!";
                return;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(startingDate) || !string.IsNullOrWhiteSpace(endingDate))
                {
                    try
                    {
                        SQLProcedures.createPutniNalog(startingPlace, endingPlace, driverID, vehicleID, startingDate, endingDate);
                        FillTravelWarrants();
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
            Place_of_Departure.Text = null;
            Destination_Place.Text = null;
            Date_of_issue.Text = null;
            Delivery_Date.Text = null;
            LblError.Text = null;
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            if (GridListOfWarrants.SelectedRow == null || GridListOfWarrants.Rows.Count == 0)
            {
                LblError.Text = "Please select a travel warrant!";
                FillTravelWarrants();
            }
            else
            {
                int idWarrant = int.Parse(GridListOfWarrants.SelectedRow.Cells[0].Text);
                string startingPlace = Place_of_Departure.Text;
                string endingPlace = Destination_Place.Text;
                int driverID = int.Parse(DdlDrivers.SelectedItem.Text);
                int vehicleID = int.Parse(DdlVehicles.SelectedItem.Text);
                DateTime startingDate = DateTime.Parse(Date_of_issue.Text);
                DateTime endingDate = DateTime.Parse(Delivery_Date.Text);

              
                    try
                    {
                        SQLProcedures.updatePutniNalog(idWarrant,startingPlace, endingPlace, driverID, vehicleID, startingDate, endingDate);
                        FillTravelWarrants();
                        ClearGrid();

                    }
                    catch (Exception ex)
                    {
                        LblError.Text = $"Exception: {ex.Message}";
                    }
                
            }
        }
        protected void Delete_Click(object sender, EventArgs e)
        {
            if (GridListOfWarrants.SelectedRow == null || GridListOfWarrants.Rows.Count == 0)
            {
                LblError.Text = "Please select a travel warrant!";
                FillTravelWarrants();
            }
            else
            {
                int idWarrant = int.Parse(GridListOfWarrants.SelectedRow.Cells[0].Text);
                try
                {
                    SQLProcedures.deletePutniNalog(idWarrant);
                    FillTravelWarrants();
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
            Response.Redirect("~/MainMenu.aspx");
        }

        protected void GridListOfWarrants_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridListOfWarrants, "Select$" + e.Row.RowIndex);
            }
        }

        protected void GridListOfWarrants_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTravelWarrants();
            foreach (GridViewRow row in GridListOfWarrants.Rows)
            {
                if (row.RowIndex == GridListOfWarrants.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("Black");
                    row.ForeColor = ColorTranslator.FromHtml("White");
                    Place_of_Departure.Text = GridListOfWarrants.SelectedRow.Cells[1].Text;
                    Destination_Place.Text = GridListOfWarrants.SelectedRow.Cells[2].Text;
                    DdlDrivers.Text = GridListOfWarrants.SelectedRow.Cells[3].Text;
                    DdlVehicles.Text = GridListOfWarrants.SelectedRow.Cells[4].Text;
                    Date_of_issue.Text = GridListOfWarrants.SelectedRow.Cells[5].Text;
                    Delivery_Date.Text = GridListOfWarrants.SelectedRow.Cells[6].Text;
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
            }
           
        }
    }
}