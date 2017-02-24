using Decision.DataLayer.Extensions;

namespace Decision.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SqlResources : DbMigrationBase
    {

        public override void Up()
        {
            ExecSqlResource("Settings");
            //ExecSqlResource("Indexes");
            //ExecSqlResource("Indexes.SqlServer");
            //ExecSqlResource("StoredProcedures");
            //ExecSqlResource("Views");
        }

        public override void Down()
        {
            //ExecSqlResource("Indexes.Inverse");
            //ExecSqlResource("Indexes.SqlServer.Inverse");
            //ExecSqlResource("StoredProcedures.Inverse");
            //ExecSqlResource("Views.Inverse");
        }
    }
}
