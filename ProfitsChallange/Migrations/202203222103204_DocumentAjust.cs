namespace ProfitsChallange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocumentAjust : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Documents", "Review", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Documents", "Review", c => c.String(nullable: false, unicode: false));
        }
    }
}
