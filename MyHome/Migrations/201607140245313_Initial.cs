namespace MyHome.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserInfo", "UserName", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.UserInfo", "IDCard", c => c.String(maxLength: 18));
            AlterColumn("dbo.UserInfo", "QQ", c => c.String(maxLength: 30));
            AlterColumn("dbo.UserInfo", "Phone", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInfo", "Phone", c => c.String());
            AlterColumn("dbo.UserInfo", "QQ", c => c.String());
            AlterColumn("dbo.UserInfo", "IDCard", c => c.String());
            AlterColumn("dbo.UserInfo", "UserName", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
