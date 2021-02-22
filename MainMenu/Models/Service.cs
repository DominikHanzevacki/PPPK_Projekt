using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MainMenu.Models
{
    [Table("Servis")]
    public class Service
    {
        public Service(int iDServis, string naziv_Servisa)
        {
            IDServis = iDServis;
            Naziv_Servisa = naziv_Servisa;
        }
        [Key]
        public int IDServis { get; set; }
        public string Naziv_Servisa { get; set; }

        public Service()
        {
        }
    }
}