using MainMenu.Models;
using MainMenu.SQL_Procedures;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml;

namespace MainMenu
{

    public partial class CRUD_Drivers : System.Web.UI.Page
    {
        public List<Drivers> listOfDrivers;
        private int counter = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDrivers();
            }
            listOfDrivers = SQLProcedures.selectSveVozace().ToList();
        }

        private void FillDrivers()
        {
            GridListofDrivers.DataSource = SQLProcedures.selectSveVozace();
            GridListofDrivers.DataBind();

        }

        protected void GridListofDrivers_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridListofDrivers.DataSource = SQLProcedures.selectSveVozace();
            GridListofDrivers.DataBind();
            foreach (GridViewRow row in GridListofDrivers.Rows)
            {
                if (row.RowIndex == GridListofDrivers.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("Black");
                    row.ForeColor = ColorTranslator.FromHtml("White");
                    InputIme.Text = GridListofDrivers.SelectedRow.Cells[1].Text;
                    InputPrezime.Text = GridListofDrivers.SelectedRow.Cells[2].Text;
                    InputBrojMobitela.Text = GridListofDrivers.SelectedRow.Cells[3].Text;
                    InputBrojVozacke.Text = GridListofDrivers.SelectedRow.Cells[4].Text;

                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
            }
        }

        protected void GridListofDrivers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridListofDrivers, "Select$" + e.Row.RowIndex);
            }
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MainMenu.aspx");
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            string name = InputIme.Text;
            string surname = InputPrezime.Text;
            string cellphone = InputBrojMobitela.Text;
            string licenseNumber = InputBrojVozacke.Text;

            bool exists = false;
            foreach (var item in listOfDrivers)
            {
                if (InputIme.Text.ToString() == item.Ime.ToString())
                {
                    exists = true;
                    break;
                }
            }
            if (exists)
            {
                FillDrivers();
                LblError.Text = "Sorry, that user already exists in the database!";
                return;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(name) || !string.IsNullOrWhiteSpace(surname) || !string.IsNullOrWhiteSpace(cellphone) || !string.IsNullOrWhiteSpace(licenseNumber))
                {
                    try
                    {
                        SQLProcedures.createVozac(name, surname, cellphone, licenseNumber);
                        FillDrivers();
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
            InputIme.Text = null;
            InputPrezime.Text = null;
            InputBrojMobitela.Text = null;
            InputBrojVozacke.Text = null;
            LblError.Text = null;
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            if (GridListofDrivers.SelectedRow == null || GridListofDrivers.Rows.Count == 0)
            {
                LblError.Text = "Please select a driver!";
                FillDrivers();
            }
            else
            {
                int idDriver = int.Parse(GridListofDrivers.SelectedRow.Cells[0].Text);
                string name = InputIme.Text;
                string surname = InputPrezime.Text;
                string cellphone = InputBrojMobitela.Text;
                string licenseNumber = InputBrojVozacke.Text;

                if (!string.IsNullOrWhiteSpace(name) || !string.IsNullOrWhiteSpace(surname) || !string.IsNullOrWhiteSpace(cellphone) || !string.IsNullOrWhiteSpace(licenseNumber))
                {
                    try
                    {
                        SQLProcedures.updateVozac(idDriver, name, surname, cellphone, licenseNumber);
                        FillDrivers();
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
            if (GridListofDrivers.SelectedRow == null || GridListofDrivers.Rows.Count == 0)
            {
                LblError.Text = "Please select a driver!";
                FillDrivers();
            }
            else
            {
                int idService = int.Parse(GridListofDrivers.SelectedRow.Cells[0].Text);
                try
                {
                    SQLProcedures.deleteServis(idService);
                    FillDrivers();
                    ClearGrid();
                }
                catch (Exception ex)
                {
                    LblError.Text = $"Exception: {ex.Message}";
                }
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string XML_PATH = "Driver" + counter + ".xml";
            DataSet ds = CreateDataSet();
            ds.WriteXml(MapPath(XML_PATH), XmlWriteMode.IgnoreSchema);
            counter++;
            FillDrivers();
            MessageBox.Show("Export Driver to XML Successful!");
        }

        private DataSet CreateDataSet()
        {
            DataSet ds = new DataSet("Vozac");
            var driverTable = new DataTable();
            driverTable.Columns.Add("IDVozac");
            driverTable.Columns.Add("Ime");
            driverTable.Columns.Add("Prezime");
            driverTable.Columns.Add("Broj_Mobitela");
            driverTable.Columns.Add("Broj_Vozacke_Dozvole");

            var newRow = driverTable.NewRow();
            newRow["IDVozac"] = GridListofDrivers.SelectedRow.Cells[0].Text;
            newRow["Ime"] = GridListofDrivers.SelectedRow.Cells[1].Text;
            newRow["Prezime"] = GridListofDrivers.SelectedRow.Cells[2].Text;
            newRow["Broj_Mobitela"] = GridListofDrivers.SelectedRow.Cells[3].Text;
            newRow["Broj_Vozacke_Dozvole"] = GridListofDrivers.SelectedRow.Cells[4].Text;
            driverTable.Rows.Add(newRow);
            ds.Tables.Add(driverTable);
            return ds;
        }
        protected void btnImport_Click(object sender, EventArgs e)
        {
            string ime;
            string prezime;
            string broj_Mobitela;
            string broj_Vozacke_Dozvole;

            using (var reader = XmlReader.Create("C:/Users/Domi/Desktop/PPPK_Projekt/MainMenu/Driver1.xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement() && reader.Name.Equals("Vozac"))
                    {

                        reader.ReadToFollowing("Ime");
                        reader.Read();
                        ime = reader.Value;
                        reader.ReadToFollowing("Prezime");
                        reader.Read();
                        prezime = reader.Value;
                        reader.ReadToFollowing("Broj_Mobitela");
                        reader.Read();
                        broj_Mobitela = reader.Value;
                        reader.ReadToFollowing("Broj_Vozacke_Dozvole");
                        reader.Read();
                        broj_Vozacke_Dozvole = reader.Value;
                        SQLProcedures.createVozac(ime, prezime, broj_Mobitela, broj_Vozacke_Dozvole);
                        
                    }
                }
            }
            FillDrivers();
            MessageBox.Show("Successfully Added Driver!");
        }
    }
}