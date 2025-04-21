namespace AutoLotDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inventory", "Count", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Customers", "LastName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Customers", "FirstName", c => c.String(maxLength: 50));
            DropColumn("dbo.Inventory", "Count");
        }
    }
}
