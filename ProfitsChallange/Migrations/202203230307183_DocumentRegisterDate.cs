namespace ProfitsChallange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocumentRegisterDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "RegisterDate", c => c.DateTime(nullable: false, precision: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "RegisterDate");
        }
    }
}
