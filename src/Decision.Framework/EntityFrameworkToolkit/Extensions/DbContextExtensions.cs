using System;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using Decision.Framework.Extensions;

namespace Decision.Framework.EntityFrameworkToolkit.Extensions
{
    public static class DbContextExtensions
    {
        public static string GetTableName(this DbContext context, Type type)
        {
            var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

            // Get the part of the model that contains info about the actual CLR types
            var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));

            // Get the entity type from the model that maps to the CLR type
            var entityType = metadata
                    .GetItems<EntityType>(DataSpace.OSpace)
                    .Single(e => objectItemCollection.GetClrType(e) == type);

            // Get the entity set that uses this entity type
            var entitySet = metadata
                .GetItems<EntityContainer>(DataSpace.CSpace)
                .Single()
                .EntitySets
                .Single(s => s.ElementType.Name == entityType.Name);

            // Find the mapping between conceptual and storage model for this entity set
            var mapping = metadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace)
                    .Single()
                    .EntitySetMappings
                    .Single(s => s.EntitySet == entitySet);

            // Find the storage entity set (table) that the entity is mapped
            var table = mapping
                .EntityTypeMappings.Single()
                .Fragments.Single()
                .StoreEntitySet;

            // Return the table name from the storage entity set
            return (string)table.MetadataProperties["Table"].Value ?? table.Name;
        }
        public static bool ColumnExists(this DbContext context, string tableName, string columnName)
        {
            if (context == null || !tableName.HasValue() || !columnName.HasValue()) return false;

            const string sql = @"Select column_name From INFORMATION_SCHEMA.COLUMNS Where table_name = @tableName And column_name = @columnName";

            var col = ((IObjectContextAdapter)context).ObjectContext.ExecuteStoreQuery<string>(sql,
                new SqlParameter("@tableName", tableName), new SqlParameter("@columnName", columnName)).FirstOrDefault();
            return col.HasValue();
        }

        public static void ColumnEnsure(this DbContext context, string tableName, string columnName, string columnDataType)
        {
            if (!context.ColumnExists(tableName, columnName))
            {
                context.Database.ExecuteSqlCommand($"ALTER TABLE {tableName} ADD {columnName} {columnDataType}");
            }
        }

        public static void ColumnDelete(this DbContext context, string tableName, string columnName)
        {
            if (context.ColumnExists(tableName, columnName))
            {
                context.Database.ExecuteSqlCommand("ALTER TABLE {0} DROP COLUMN {1}".FormatWith(tableName, columnName));
            }
        }

        public static bool DatabaseExists(this DbContext context, string databaseName)
        {
            if (context == null || !databaseName.HasValue()) return false;

            const string sql = @"Select database_id From sys.databases Where Name = @databaseName";
            return
                ((IObjectContextAdapter) context).ObjectContext.ExecuteStoreQuery<int>(sql,
                    new SqlParameter("@databaseName", databaseName)).Any();
        }

        public static int InsertInto(this DbContext context, string sql, params object[] parameters)
        {
            return (int)context.Database.SqlQuery<decimal>(sql + "; SELECT @@IDENTITY;", parameters).FirstOrDefault();
        }

        public static int Execute(this DbContext context, string sql, params object[] parameters)
        {
            return context.Database.ExecuteSqlCommand(sql, parameters);
        }

        public static SqlServerInfo GetSqlServerInfo(this DbContext context)
        {
            const string sql = @"SELECT  
                    SERVERPROPERTY('productversion') as 'ProductVersion', 
                    SERVERPROPERTY('productlevel') as 'PatchLevel',  
                    SERVERPROPERTY('edition') as 'ProductEdition',
                    SERVERPROPERTY('buildclrversion') as 'ClrVersion',
                    SERVERPROPERTY('collation') as 'DefaultCollation',
                    SERVERPROPERTY('instancename') as 'Instance',
                    SERVERPROPERTY('lcid') as 'Lcid',
                    SERVERPROPERTY('servername') as 'ServerName'";

            return context.Database.SqlQuery<SqlServerInfo>(sql).FirstOrDefault();
        }
    }
}
