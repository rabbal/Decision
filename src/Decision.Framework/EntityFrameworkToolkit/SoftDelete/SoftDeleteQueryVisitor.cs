using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace Decision.Framework.EntityFrameworkToolkit.SoftDelete
{
    internal class SoftDeleteQueryVisitor : DefaultExpressionVisitor
    {
        public override DbExpression Visit(DbScanExpression expression)
        {
            var column = SoftDeleteAttribute.GetSoftDeleteColumnName(expression.Target.ElementType);
            if (column == null) return base.Visit(expression);

            var table = (EntityType)expression.Target.ElementType;
            if (table.Properties.All(p => p.Name != column)) return base.Visit(expression);

            var binding = DbExpressionBuilder.Bind(expression);

            return DbExpressionBuilder.Filter(
                binding,
                DbExpressionBuilder.NotEqual(
                    DbExpressionBuilder.Property(
                        DbExpressionBuilder.Variable(binding.VariableType, binding.VariableName),
                        column),
                    DbExpression.FromBoolean(true)));
        }
    }
}
