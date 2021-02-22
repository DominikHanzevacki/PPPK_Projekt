using MainMenu.Models;
using MainMenu.SQL_Procedures;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Windows.Forms;

namespace MainMenu.HTML
{
    public class HTMLCreate
    { 
        private static int id = 1;
        private static List<Service> services = new List<Service>();
        internal static void createHTML(List<ServiseBill> listOfBills, Vehicles vehicle)
        {
            services = SQLProcedures.selectSveServise().ToList();
            StreamWriter writer = new StreamWriter($"C:\\Users\\Domi\\Desktop\\PPPK_Projekt\\MainMenu\\Racun{id}.html");
            writer.WriteLine("<html>");
            writer.WriteLine("<body>");
            writer.WriteLine("<p> HTML Izvjestaj</p>");
            writer.WriteLine("<table>");
            writer.WriteLine("<tr>");
            writer.WriteLine("<td>Vozilo:" + " " + $"{vehicle.IDVozilo}" + "</td>");
            writer.WriteLine("</tr>");
            writer.WriteLine("<tr>");
            writer.WriteLine("<td>Tip:" + " " + $"{vehicle.Tip}" + "</td>");
            writer.WriteLine("</tr>");
            writer.WriteLine("<tr>");
            writer.WriteLine("<td>Marka:" + " " + $"{vehicle.Marka}" + "</td>");
            writer.WriteLine("</tr>");
            writer.WriteLine("<tr>");
            writer.WriteLine("<td>Inicijalno_Stanje_Kilometara:" + " " + $"{vehicle.Inicijalno_Stanje_Kilometara}" + "</td>");
            writer.WriteLine("</tr>");
            writer.WriteLine("<tr>");
            writer.WriteLine("<td>Godina_Proizvodnje:" + " " + $"{vehicle.Godina_Proizvodnje}" + "</td>");
            writer.WriteLine("</tr>");
            foreach (ServiseBill item in listOfBills)
            {
                foreach (Service service in services)
                {
                    if (service.IDServis == item.ServisID)
                    {
                        writer.WriteLine("<tr>");
                        writer.WriteLine($"<td>Servis {id}:" + " "  + $"{service.Naziv_Servisa}" + "</td>");
                        writer.WriteLine("</tr>");
                        id++;
                    }
                }
            }
            writer.WriteLine("</table>");
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
            writer.Close();
            MessageBox.Show("HTML file successfully created!");
        }
    }
}