using MainMenu.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MainMenu.DataAccessObject
{
    public class Context : DbContext
    {
        public Context() : base("name=cs")
        {

        }
        public DbSet<Vehicles> Vozila { get; set; }
        public DbSet<Service> Servisi { get; set; }
        public DbSet<ServiseBill> Racuni { get; set; }
    }
}