namespace App.MessageBus.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeventsubcriber : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventSubcribers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Key = c.String(),
                        Uri = c.String(),
                        Status = c.Int(nullable: false),
                        ModuleName = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EventSubcribers");
        }
    }
}
