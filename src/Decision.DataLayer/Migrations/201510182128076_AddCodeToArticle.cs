namespace Decision.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCodeToArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Code", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Code");
        }
    }
}
