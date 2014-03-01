namespace ParkingHouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurrentProfits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CurrentSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalCars = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Cars", "EntryTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "EntryTime");
            DropTable("dbo.CurrentProfits");
        }
    }
}
