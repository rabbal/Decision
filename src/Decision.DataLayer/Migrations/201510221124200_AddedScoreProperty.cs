namespace Decision.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedScoreProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "Score", c => c.Decimal(nullable: false, precision: 7, scale: 2));
            AlterColumn("dbo.Applicants", "TrainingGPA", c => c.Decimal(nullable: false, precision: 7, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Applicants", "TrainingGPA", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Applicants", "Score");
        }
    }
}
