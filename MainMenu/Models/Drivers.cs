using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainMenu.Models
{
    public class Drivers
    {
        public Drivers(int iDVozac, string ime, string prezime, string broj_Mobitela, string broj_Vozacke_Dozvole)
        {
            IDVozac = iDVozac;
            Ime = ime;
            Prezime = prezime;
            Broj_Mobitela = broj_Mobitela;
            Broj_Vozacke_Dozvole = broj_Vozacke_Dozvole;
        }

        public int IDVozac { get; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Broj_Mobitela { get; set; }
        public string Broj_Vozacke_Dozvole { get; set; }
    }
}