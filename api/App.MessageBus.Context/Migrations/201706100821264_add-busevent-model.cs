namespace App.MessageBus.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addbuseventmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusEventAggregates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content_Key = c.String(),
                        Content_Content = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BusEventAggregates");
        }
    }
}
