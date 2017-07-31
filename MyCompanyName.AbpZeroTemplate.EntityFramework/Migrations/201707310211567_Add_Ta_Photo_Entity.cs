namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Ta_Photo_Entity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(nullable: false),
                        Url = c.String(nullable: false),
                        PhotoCategoryId = c.Long(nullable: false),
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
                    { "DynamicFilter_Photo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .ForeignKey("dbo.PhotoCategories", t => t.PhotoCategoryId, cascadeDelete: true)
                .Index(t => t.PhotoCategoryId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "PhotoCategoryId", "dbo.PhotoCategories");
            DropForeignKey("dbo.Photos", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.Photos", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.Photos", "CreatorUserId", "dbo.AbpUsers");
            DropIndex("dbo.Photos", new[] { "CreatorUserId" });
            DropIndex("dbo.Photos", new[] { "LastModifierUserId" });
            DropIndex("dbo.Photos", new[] { "DeleterUserId" });
            DropIndex("dbo.Photos", new[] { "PhotoCategoryId" });
            DropTable("dbo.Photos",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Photo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
