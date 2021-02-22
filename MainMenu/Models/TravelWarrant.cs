using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainMenu.Models
{
    public class TravelWarrant
    {
        public TravelWarrant(int iDPutniNalog, string mjesto_Polaska, string mjesto_Putovanja, int vozacID, int voziloID, DateTime datum_Izdavanja, DateTime datum_Predaje, string tip_Putnog_Naloga)
        {
            IDPutniNalog = iDPutniNalog;
            Mjesto_Polaska = mjesto_Polaska;
            Mjesto_Putovanja = mjesto_Putovanja;
            VozacID = vozacID;
            VoziloID = voziloID;
            Datum_Izdavanja = datum_Izdavanja;
            Datum_Predaje = datum_Predaje;
            Tip_Putnog_Naloga = tip_Putnog_Naloga;
        }

        public enum Tip
        {
            Aktivan,
            Zatvoren,
            Buduci
        }
        public int IDPutniNalog { get; }
        public string Mjesto_Polaska { get; set; }
        public string Mjesto_Putovanja { get; set; }
        public int VozacID { get; set; }
        public int VoziloID { get; set; }
        public DateTime Datum_Izdavanja { get; set; }
        public DateTime Datum_Predaje { get; set; }
        public string Tip_Putnog_Naloga { get; set; }
    }
}