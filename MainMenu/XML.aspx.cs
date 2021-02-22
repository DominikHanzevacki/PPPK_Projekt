using MainMenu.Models;
using MainMenu.SQL_Procedures;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace MainMenu
{
    public partial class XML : System.Web.UI.Page
    {
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        private const string SELECT_ALL_TRAVEL_WARRANTS = "selectSve";
        private const string PUTNI_NALOG_RELATION_DRIVER = "putniNalog_Vozac";
        private const string PUTNI_NALOG_RELATION_VEHICLES = "putniNalog_Vozilo";
        private const string XML_PATH = "Backup.xml";
        private const string XML_PATH_CLEANED = "BackupForRestoringDatabase.xml";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSQLtoXML_Click(object sender, EventArgs e)
        {
            try
            {
                CreateXml();
                btnSQLtoXML.Enabled = false;
                btnSQLtoXML.Visible = false;
            }
            catch (Exception ex)
            {
                LblInfo.Text = ex.Message;
            }
        }

        private void CreateXml()
        {
            DataSet ds = CreateDataSet();
            ds.WriteXml(MapPath(XML_PATH), XmlWriteMode.WriteSchema);

            LblInfo.Text = "Xml file saved!";
            btnShowXML.Visible = true;
        }

        private DataSet CreateDataSet()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlDataAdapter da = new SqlDataAdapter(SELECT_ALL_TRAVEL_WARRANTS, con);
                DataSet ds = new DataSet("Route_Data");
                da.Fill(ds);
                ds.Tables[0].TableName = nameof(TravelWarrant);
                ds.Tables[1].TableName = nameof(Drivers);
                ds.Tables[2].TableName = nameof(Vehicles);
                return ds;
            }
        }



        private void ShowRouteData()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(MapPath(XML_PATH));
            StringBuilder report = new StringBuilder();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                report.Append($"Putni Nalog: {(int)ds.Tables["TravelWarrant"].Rows[i]["IDPutniNalog"]}<br/>[ ");
                report.Append($"Mjesto Polaska: " +
                    $"{(string)ds.Tables["TravelWarrant"].Rows[i]["Mjesto_Polaska"].ToString()} -> " +
                    $"{(string)ds.Tables["TravelWarrant"].Rows[i]["Mjesto_Putovanja"].ToString()}<br/>");
                foreach (DataRow item in ds.Tables["Drivers"].Rows)
                {
                    if ((int)ds.Tables["TravelWarrant"].Rows[i]["VozacID"] == (int)item[0])
                    {

                        report.Append($"Vozac: {(string)ds.Tables["Drivers"].Rows[(int)item[0] - 1]["Ime"] + " " + (string)ds.Tables["Drivers"].Rows[(int)item[0] - 1]["Prezime"]} <br/>");
                    }
                }
                foreach (DataRow item in ds.Tables["Vehicles"].Rows)
                {
                    if ((int)ds.Tables["TravelWarrant"].Rows[i]["VoziloID"] == (int)item[0])
                    {
                        report.Append($"Vozilo: {(string)ds.Tables["Vehicles"].Rows[i]["Tip"]} ] <br/>");
                    }
                }
                report.Append("<br /><br />");

            }
            LblResult.Text = report.ToString();
        }

        protected void btnShowXML_Click(object sender, EventArgs e)
        {
            try
            {
                ShowRouteData();
            }
            catch (Exception ex)
            {
                LblInfo.Text = ex.Message;
            }
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MainMenu.aspx");
        }

        protected void btnResetDatabase_Click(object sender, EventArgs e)
        {
            SQLProcedures.resetDatabase();
            LblInfo.Text = "Database is now Deleted!";
        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            using (XmlWriter xmlWriter = CreateWriter())
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Route_Data");

                DataSet dtContacts = CreateDataSet();


                dtContacts.Tables[1].Rows.Cast<DataRow>()
                .ToList()
                .ForEach(dr =>
                {
                    //Vozac
                    xmlWriter.WriteStartElement("Vozac");
                    xmlWriter.WriteAttributeString("IDVozac", dr[nameof(Drivers.IDVozac)].ToString());

                    xmlWriter.WriteStartElement("Ime");
                    xmlWriter.WriteString(dr[nameof(Drivers.Ime)].ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Prezime");
                    xmlWriter.WriteString(dr[nameof(Drivers.Prezime)].ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Broj_Mobitela");
                    xmlWriter.WriteString(dr[nameof(Drivers.Broj_Mobitela)].ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Broj_Vozacke_Dozvole");
                    xmlWriter.WriteString(dr[nameof(Drivers.Broj_Vozacke_Dozvole)].ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                });
                dtContacts.Tables[2].Rows.Cast<DataRow>()
                .ToList()
                .ForEach(dr =>
                {
                    //Vozilo
                    xmlWriter.WriteStartElement("Vozilo");
                    xmlWriter.WriteAttributeString("IDVozilo", dr[nameof(Vehicles.IDVozilo)].ToString());

                    xmlWriter.WriteStartElement("Tip");
                    xmlWriter.WriteString(dr[nameof(Vehicles.Tip)].ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Marka");
                    xmlWriter.WriteString(dr[nameof(Vehicles.Marka)].ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Godina_Proizvodnje");
                    xmlWriter.WriteString(dr[nameof(Vehicles.Godina_Proizvodnje)].ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Inicijalno_Stanje_Kilometara");
                    xmlWriter.WriteString(dr[nameof(Vehicles.Inicijalno_Stanje_Kilometara)].ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();

                });
                dtContacts.Tables[0].Rows.Cast<DataRow>()
                   .ToList()
                   .ForEach(dr =>
                   {
                       //Putni Nalog
                       xmlWriter.WriteStartElement("PutniNalog");
                       xmlWriter.WriteAttributeString("IDPutniNalog", dr[nameof(TravelWarrant.IDPutniNalog)].ToString());

                       xmlWriter.WriteStartElement("Mjesto_Polaska");
                       xmlWriter.WriteString(dr[nameof(TravelWarrant.Mjesto_Polaska)].ToString());
                       xmlWriter.WriteEndElement();

                       xmlWriter.WriteStartElement("Mjesto_Putovanja");
                       xmlWriter.WriteString(dr[nameof(TravelWarrant.Mjesto_Putovanja)].ToString());
                       xmlWriter.WriteEndElement();

                       xmlWriter.WriteStartElement("VozacID");
                       xmlWriter.WriteString(dr[nameof(TravelWarrant.VozacID)].ToString());
                       xmlWriter.WriteEndElement();

                       xmlWriter.WriteStartElement("VoziloID");
                       xmlWriter.WriteString(dr[nameof(TravelWarrant.VoziloID)].ToString());
                       xmlWriter.WriteEndElement();

                       xmlWriter.WriteStartElement("Datum_Izdavanja");
                       xmlWriter.WriteString(dr[nameof(TravelWarrant.Datum_Izdavanja)].ToString());
                       xmlWriter.WriteEndElement();

                       xmlWriter.WriteStartElement("Datum_Predaje");
                       xmlWriter.WriteString(dr[nameof(TravelWarrant.Datum_Predaje)].ToString());
                       xmlWriter.WriteEndElement();

                       xmlWriter.WriteEndElement();
                   });
                xmlWriter.WriteEndElement();
                LblInfo.Text = "Backup Created Successfully!";
            }
        }

        private XmlWriter CreateWriter()
        {
            XmlWriterSettings postavke = new XmlWriterSettings
            {
                Indent = true
            };
            return XmlWriter.Create(MapPath(XML_PATH_CLEANED), postavke);
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            //Putni Nalog
            string mjesto_Polaska;
            string mjesto_Putovanja;
            int vozacID;
            int voziloID;
            string datum_Izdavanja;
            string datum_Predaje;
            //Vozac
            string ime;
            string prezime;
            string broj_Mobitela;
            string broj_Vozacke_Dozvole;
            //Vozilo
            string tip;
            string marka;
            int godina_Proizvodnje;
            int inicijalno_Stanje_Kilometara;
            List<String> values = new List<string>();
            using (var reader = XmlReader.Create("C:/Users/Domi/Desktop/PPPK_Projekt - Copy/PPPK_Projekt/MainMenu/BackupForRestoringDatabase.xml"))
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
                    else if (reader.IsStartElement() && reader.Name.Equals("Vozilo"))
                    {

                        reader.ReadToFollowing("Tip");
                        reader.Read();
                        tip = reader.Value;
                        reader.ReadToFollowing("Marka");
                        reader.Read();
                        marka = reader.Value;
                        reader.ReadToFollowing("Godina_Proizvodnje");
                        reader.Read();
                        godina_Proizvodnje = Int32.Parse(reader.Value);
                        reader.ReadToFollowing("Inicijalno_Stanje_Kilometara");
                        reader.Read();
                        inicijalno_Stanje_Kilometara = Int32.Parse(reader.Value);
                        SQLProcedures.createVozilo(tip, marka, godina_Proizvodnje, inicijalno_Stanje_Kilometara);



                    }
                    else if (reader.IsStartElement() && reader.Name.Equals("PutniNalog"))
                    {

                        reader.ReadToFollowing("Mjesto_Polaska");
                        reader.Read();
                        mjesto_Polaska = reader.Value;
                        reader.ReadToFollowing("Mjesto_Putovanja");
                        reader.Read();
                        mjesto_Putovanja = reader.Value;
                        reader.ReadToFollowing("VozacID");
                        reader.Read();
                        vozacID = Int32.Parse(reader.Value);
                        reader.ReadToFollowing("VoziloID");
                        reader.Read();
                        voziloID = Int32.Parse(reader.Value);
                        reader.ReadToFollowing("Datum_Izdavanja");
                        reader.Read();
                        datum_Izdavanja = reader.Value;
                        reader.ReadToFollowing("Datum_Predaje");
                        reader.Read();
                        datum_Predaje = reader.Value;
                        SQLProcedures.createPutniNalog(mjesto_Polaska, mjesto_Putovanja, vozacID, voziloID, datum_Izdavanja, datum_Predaje);

                    }

                }
                LblInfo.Text = "Database restored Successfully!";


            }
        }



    }

}