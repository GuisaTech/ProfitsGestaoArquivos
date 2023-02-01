using MySql.Data.EntityFramework;
using ProfitsChallange.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProfitsChallange.DataContext
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]

    public class Context : DbContext
    {
        public Context() : base("ProfitsChallangeDB")
        {}

        public DbSet<Document> Document { get; set; }

    }
}