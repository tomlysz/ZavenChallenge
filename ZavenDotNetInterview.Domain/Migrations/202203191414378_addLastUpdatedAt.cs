namespace ZavenDotNetInterview.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLastUpdatedAt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "LastUpdatedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "LastUpdatedAt");
        }
    }
}
