using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MainMenu.Models
{
    [Table("Vozilo")]
    public class Vehicles
    {
        public Vehicles(int iDVozilo, string tip, string marka, int godina_Proizvodnje, int inicijalno_Stanje_Kilometara)
        {
            IDVozilo = iDVozilo;
            Tip = tip;
            Marka = marka;
            Godina_Proizvodnje = godina_Proizvodnje;
            Inicijalno_Stanje_Kilometara = inicijalno_Stanje_Kilometara;
        }
        [Key]
        public int IDVozilo { get; set; }
        public string Tip { get; set; }
        public string Marka { get; set; }
        public int Godina_Proizvodnje { get; set; }
        public int Inicijalno_Stanje_Kilometara { get; set; }

        public Vehicles()
        {
        }
    }
}