namespace ParkingHouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarID = c.Int(nullable: false, identity: true),
                        CarLength = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CarWidth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HasContract = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CarID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cars");
        }
    }
}
