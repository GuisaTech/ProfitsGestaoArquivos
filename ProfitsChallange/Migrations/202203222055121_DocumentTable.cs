namespace ProfitsChallange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocumentTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        Title = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Review = c.String(nullable: false, unicode: false),
                        PlannedDate = c.DateTime(nullable: false, precision: 0),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Removed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Documents");
        }
    }
}
