using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_Asses1CodeFirst.Models;
using System.Data.Entity;

namespace MVC_Asses1CodeFirst.Models
{
    public class MovieDBContext : DbContext
    {
        public MovieDBContext() : base("connectstr")
        { }
        public DbSet<Models.Movies> Movies { get; set; }
    }
}