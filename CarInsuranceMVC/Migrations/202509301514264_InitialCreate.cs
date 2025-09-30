namespace CarInsuranceMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Insurees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        EmailAddress = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        CarYear = c.Int(nullable: false),
                        CarMake = c.String(nullable: false),
                        CarModel = c.String(nullable: false),
                        SpeedingTickets = c.Int(nullable: false),
                        DUI = c.Boolean(nullable: false),
                        CoverageType = c.Boolean(nullable: false),
                        Quote = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Insurees");
        }
    }
}
