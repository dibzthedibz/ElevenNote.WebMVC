namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryAddedForeignKeyAddedToNote : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 16),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            AddColumn("dbo.Note", "CategoryId", c => c.Int());
            CreateIndex("dbo.Note", "CategoryId");
            AddForeignKey("dbo.Note", "CategoryId", "dbo.Category", "CategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Note", "CategoryId", "dbo.Category");
            DropIndex("dbo.Note", new[] { "CategoryId" });
            DropColumn("dbo.Note", "CategoryId");
            DropTable("dbo.Category");
        }
    }
}