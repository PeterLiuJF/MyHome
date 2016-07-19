using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MyHome.Models
{
    public class HomeContext : DbContext
    {
        public DbSet<UserInfo> UserInfo { get; set; }
        public HomeContext() : base("HomeContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<MyHome.Models.AddressInfo> AddressInfoes { get; set; }
    }
}