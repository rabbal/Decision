using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;

namespace Decision.Framework.EntityFrameworkToolkit.Indexing
{
    public class CreateIncludedColumnsIndexOperation : MigrationOperation
    {
        public CreateIncludedColumnsIndexOperation(object anonymousArguments = null) : base(anonymousArguments)
        {
        }

        public override bool IsDestructiveChange => false;
        public bool IsUnique { get; set; }
        public string Table { get; set; }
        public string Name { get; set; }
        public IList<string> Columns { get; set; }
        public bool IsClustered { get; set; }
        public IList<string> IncludedColumns { get; set; }

    }
}
