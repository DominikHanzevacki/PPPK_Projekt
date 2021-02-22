using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MainMenu.Models
{
    [Table("Racun")]
    public class ServiseBill
    {
        [Key]
        public int IDRacun { get; set; }
        [ForeignKey("Services")]
        public int ServisID { get; set; }
        [ForeignKey("Vehicles")]
        public int VoziloID { get; set; }
        public virtual IList<Vehicles> Vehicles { get; set; }
        public virtual IList<Service> Services { get; set; }

        public ServiseBill(int iDRacun, int servisID, int voziloID)
        {
            IDRacun = iDRacun;
            ServisID = servisID;
            VoziloID = voziloID;
        }
        public ServiseBill()
        {
        }
    }
}