using MyHome.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace MyHome.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<HomeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "MyHome.Models.HomeContext";
        }

        protected override void Seed(HomeContext context)
        {
            //base.Seed(context);
        }
    }
}