namespace Decision.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveConstraint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EducationalBackgrounds", "Advisor", c => c.String(maxLength: 50));
            AlterColumn("dbo.EducationalBackgrounds", "Supervisor", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EducationalBackgrounds", "Supervisor", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.EducationalBackgrounds", "Advisor", c => c.String(nullable: false, maxLength: 256));
        }
    }
}
