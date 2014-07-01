using System.Data;
using System.Xml;
using System.Data.Common;
using CodePorter.DataAccess;

namespace CodePorter.Core
{
    public class DbTableSource : IDataSource
    {
        public XmlDocument GetSchema(string baseName)
        {
            string sqlString = string.Format("SELECT TOP 1 * FROM [{0}]", baseName);
            string dessqlString = string.Format( "SELECT [Table Name] = i_s.TABLE_NAME,  [Column Name] = i_s.COLUMN_NAME,  [Description] = s.value FROM  INFORMATION_SCHEMA.COLUMNS i_s LEFT OUTER JOIN  sys.extended_properties s ON  s.major_id = OBJECT_ID(i_s.TABLE_SCHEMA+'.'+i_s.TABLE_NAME)  AND s.minor_id = i_s.ORDINAL_POSITION   AND s.name = 'MS_Description' WHERE  OBJECTPROPERTY(OBJECT_ID(i_s.TABLE_SCHEMA+'.'+i_s.TABLE_NAME), 'IsMsShipped')=0 AND i_s.TABLE_NAME = '{0}' ORDER BY  i_s.TABLE_NAME, i_s.ORDINAL_POSITION",baseName);
            DbAccessCommand dbcmd = CodeGenerator.Instance.GetDbAccessCommand();
            dbcmd.AttachAccessInfo(sqlString, new DbParameter[0]);
            DataTable schemaTable = dbcmd.ExecuteSchemaTable();
            dbcmd.AttachAccessInfo(dessqlString, new DbParameter[0]);
            DataTable DescriptionTable = dbcmd.ExecuteDataTable();
            return CodeGenerator.Instance.CreateDbSchemaDocument(schemaTable, baseName, DescriptionTable);
        }
    }
}

