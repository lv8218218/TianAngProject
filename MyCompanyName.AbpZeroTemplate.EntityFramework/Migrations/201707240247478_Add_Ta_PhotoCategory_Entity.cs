namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Ta_PhotoCategory_Entity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhotoCategories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PhotoCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhotoCategories", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.PhotoCategories", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.PhotoCategories", "CreatorUserId", "dbo.AbpUsers");
            DropIndex("dbo.PhotoCategories", new[] { "CreatorUserId" });
            DropIndex("dbo.PhotoCategories", new[] { "LastModifierUserId" });
            DropIndex("dbo.PhotoCategories", new[] { "DeleterUserId" });
            DropTable("dbo.PhotoCategories",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PhotoCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
