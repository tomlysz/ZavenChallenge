﻿namespace ZavenDotNetInterview.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmptyMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Status = c.Int(nullable: false),
                        DoAfter = c.DateTime(),
                        LastUpdatedAt = c.DateTime(),
                        FailedAttemptionCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        JobId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", "JobId", "dbo.Jobs");
            DropIndex("dbo.Logs", new[] { "JobId" });
            DropTable("dbo.Logs");
            DropTable("dbo.Jobs");
        }
    }
}