namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoteAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Note", "Title", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.Note", "Content", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Note", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Note", "Title", c => c.String(nullable: false));
        }
    }
}
