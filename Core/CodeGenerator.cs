#region

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.Linq;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using CodePorter.DataAccess;
using CodePorter.Utility;

#endregion

namespace CodePorter.Core
{
    public class CodeGenerator
    {
        private readonly List<string> Keywords = new List<string>();
        private readonly Dictionary<SqlDbType, int> ParameterCounter = new Dictionary<SqlDbType, int>();
        private readonly Dictionary<Type, DbType> TypeMap = new Dictionary<Type, DbType>();

        public CodeGenerator()
        {
            ParameterCounter[SqlDbType.BigInt] = 0;
            ParameterCounter[SqlDbType.Binary] = 1;
            ParameterCounter[SqlDbType.Bit] = 0;
            ParameterCounter[SqlDbType.Char] = 1;
            ParameterCounter[SqlDbType.DateTime] = 0;
            ParameterCounter[SqlDbType.Decimal] = 2;
            ParameterCounter[SqlDbType.Float] = 0;
            ParameterCounter[SqlDbType.Image] = 0;
            ParameterCounter[SqlDbType.Int] = 0;
            ParameterCounter[SqlDbType.Money] = 0;
            ParameterCounter[SqlDbType.NChar] = 1;
            ParameterCounter[SqlDbType.NText] = 0;
            ParameterCounter[SqlDbType.NVarChar] = 1;
            ParameterCounter[SqlDbType.Real] = 0;
            ParameterCounter[SqlDbType.UniqueIdentifier] = 0;
            ParameterCounter[SqlDbType.SmallDateTime] = 0;
            ParameterCounter[SqlDbType.SmallInt] = 0;
            ParameterCounter[SqlDbType.SmallMoney] = 0;
            ParameterCounter[SqlDbType.Text] = 0;
            ParameterCounter[SqlDbType.Timestamp] = 0;
            ParameterCounter[SqlDbType.TinyInt] = 0;
            ParameterCounter[SqlDbType.VarBinary] = 1;
            ParameterCounter[SqlDbType.VarChar] = 1;
            ParameterCounter[SqlDbType.Variant] = 0;
            ParameterCounter[SqlDbType.Xml] = 0;
            ParameterCounter[SqlDbType.Udt] = 0;
            Keywords =
                new List<string>(
                    "abstract,event,new,struct,as,explicit,null,switch,base,extern,object,this,bool,false,operator,throw,break,finally,out,true,byte,fixed,override,try,case,float,params,typeof,catch,for,private,uint,char,foreach,protected,ulong,checked,goto,public,unchecked,class,if,readonly,unsafe,const,implicit,ref,ushort,continue,in,return,using,decimal,int,sbyte,virtual,default,interface,sealed,volatile,delegate,internal,short,void,do,is,sizeof,while,double,lock,stackalloc,else,long,static,enum,namespace,string"
                        .Split(new[] {','}));


            TypeMap[typeof (byte)] = DbType.Byte;
            TypeMap[typeof (sbyte)] = DbType.SByte;
            TypeMap[typeof (short)] = DbType.Int16;
            TypeMap[typeof (ushort)] = DbType.UInt16;
            TypeMap[typeof (int)] = DbType.Int32;
            TypeMap[typeof (uint)] = DbType.UInt32;
            TypeMap[typeof (long)] = DbType.Int64;
            TypeMap[typeof (ulong)] = DbType.UInt64;
            TypeMap[typeof (float)] = DbType.Single;
            TypeMap[typeof (double)] = DbType.Double;
            TypeMap[typeof (decimal)] = DbType.Decimal;
            TypeMap[typeof (bool)] = DbType.Boolean;
            TypeMap[typeof (string)] = DbType.String;
            TypeMap[typeof (char)] = DbType.StringFixedLength;
            TypeMap[typeof (Guid)] = DbType.Guid;
            TypeMap[typeof (DateTime)] = DbType.DateTime;
            TypeMap[typeof (DateTimeOffset)] = DbType.DateTimeOffset;
            TypeMap[typeof (byte[])] = DbType.Binary;
            TypeMap[typeof (byte?)] = DbType.Byte;
            TypeMap[typeof (sbyte?)] = DbType.SByte;
            TypeMap[typeof (short?)] = DbType.Int16;
            TypeMap[typeof (ushort?)] = DbType.UInt16;
            TypeMap[typeof (int?)] = DbType.Int32;
            TypeMap[typeof (uint?)] = DbType.UInt32;
            TypeMap[typeof (long?)] = DbType.Int64;
            TypeMap[typeof (ulong?)] = DbType.UInt64;
            TypeMap[typeof (float?)] = DbType.Single;
            TypeMap[typeof (double?)] = DbType.Double;
            TypeMap[typeof (decimal?)] = DbType.Decimal;
            TypeMap[typeof (bool?)] = DbType.Boolean;
            TypeMap[typeof (char?)] = DbType.StringFixedLength;
            TypeMap[typeof (Guid?)] = DbType.Guid;
            TypeMap[typeof (DateTime?)] = DbType.DateTime;
            TypeMap[typeof (DateTimeOffset?)] = DbType.DateTimeOffset;
            TypeMap[typeof (Binary)] = DbType.Binary;
        }

        public NameValueCollection CurrentSettings { get; set; }

        public static CodeGenerator Instance
        {
            get { return Singleton<CodeGenerator>.GetInstance(); }
        }

        public static NameValueCollection QuerySettings
        {
            get { return (GetSection("QuerySettings") as NameValueCollection); }
        }

        public XmlDocument CreateDbSchemaDocument(DataTable schemaTable, string baseName)
        {
            return CreateDbSchemaDocument(schemaTable, baseName, null);
        }

        public XmlDocument CreateDbSchemaDocument(DataTable schemaTable, string baseName, DataTable descriptiontable)
        {
            DataColumn colProviderType = schemaTable.Columns["ProviderType"];
            DataColumn colDataType = schemaTable.Columns["DataType"];
            DataColumn colSystemDataType = GetSchemaDataColumn(ref schemaTable, "SystemDataType", typeof (string));
            DataColumn colColumnName = schemaTable.Columns["ColumnName"];
            DataColumn colAllowDBNull = schemaTable.Columns["AllowDBNull"];
            DataColumn colDataTypeName = schemaTable.Columns["DataTypeName"];
            DataColumn colShortDataType = GetSchemaDataColumn(ref schemaTable, "ShortDataType", typeof (string));
            DataColumn colIsClass = GetSchemaDataColumn(ref schemaTable, "IsClass", typeof (bool));
            DataColumn colParameterCounter = GetSchemaDataColumn(ref schemaTable, "ParameterCounter", typeof (string));
            DataColumn colProviderTypeName = GetSchemaDataColumn(ref schemaTable, "ProviderTypeName", typeof (string));
            DataColumn colDataTypeFullName = GetSchemaDataColumn(ref schemaTable, "DataTypeFullName", typeof (string));
            DataColumn colDBDataTypeFullName = GetSchemaDataColumn(ref schemaTable, "DBDataTypeFullName",
                                                                   typeof (string));
            DataColumn colProviderDataType = GetSchemaDataColumn(ref schemaTable, "ProviderDataType", typeof (string));
            DataColumn colPropertyName = GetSchemaDataColumn(ref schemaTable, "PropertyName", typeof (string));
            DataColumn colProviderSpecificDataType = schemaTable.Columns["ProviderSpecificDataType"];
            DataColumn colBaseColumnName = schemaTable.Columns["BaseColumnName"];
            if (colBaseColumnName != null)
            {
                schemaTable.Columns.Remove(colBaseColumnName);
            }
            colBaseColumnName = GetSchemaDataColumn(ref schemaTable, "BaseColumnName", typeof (string));
            foreach (DataRow row in schemaTable.Rows)
            {
                var columnName = row[colColumnName] as string;
                if (string.IsNullOrEmpty(columnName))
                {
                    row.Delete();
                    continue;
                }
                var type = (Type) row[colDataType];
                row[colIsClass] = type.IsClass;
                row[colSystemDataType] = type.FullName;
                string typeName = type.Name;
                string[] typeParts = typeName.Split(new[] {'.'});
                if (((typeParts.Length == 2) && (typeParts[0] == "System")) &&
                    (type.Assembly.FullName == typeof (int).Assembly.FullName))
                {
                    typeName = typeParts[1];
                }
                var allowDbNull = (bool) row[colAllowDBNull];
                if (type.IsClass)
                {
                    row[colShortDataType] = typeName;
                }
                else
                {
                    row[colShortDataType] = allowDbNull ? (typeName + "?") : typeName;
                }
                var providerType = (SqlDbType) row[colProviderType];
                int paramType = ParameterCounter[providerType];
                row[colParameterCounter] = paramType;
                row[colProviderTypeName] = string.Format("SqlDbType.{0}", providerType.ToString());
                DbType dbType = TypeMap[type];
                row[colDBDataTypeFullName] = string.Format("DbType.{0}", dbType.ToString());
                string dataTypeName = row[colDataTypeName].ToString();
                if (dataTypeName.Equals(providerType.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    dataTypeName = providerType.ToString();
                }
                string dataTypeFullName = string.Empty;
                switch (paramType)
                {
                    case 0:
                        dataTypeFullName = dataTypeName;
                        break;

                    case 1:
                        dataTypeFullName = string.Format("{0}({1})", dataTypeName, row["ColumnSize"]);
                        break;

                    case 2:
                        dataTypeFullName = string.Format("{0}({1},{2})", dataTypeName, row["NumericPrecision"],
                                                         row["NumericScale"]);
                        break;
                }
                row[colDataTypeFullName] = dataTypeFullName;
                if (colProviderSpecificDataType != null)
                {
                    row[colProviderDataType] = ((Type) row[colProviderSpecificDataType]).FullName;
                }
                string propertyName = GetCSharpName(columnName);
                if (schemaTable.Select(string.Format("PropertyName='{0}'", propertyName)).Length > 0)
                {
                    propertyName = propertyName + "1";
                }
                row[colPropertyName] = propertyName;
                if (columnName != propertyName)
                {
                    row[colBaseColumnName] = GetDbName(columnName);
                }
                else
                {
                    row[colBaseColumnName] = columnName;
                }
            }
            schemaTable.Columns.Remove(colDataType);
            colSystemDataType.ColumnName = "DataType";
            if (colProviderSpecificDataType != null)
            {
                schemaTable.Columns.Remove(colProviderSpecificDataType);
            }
            if (descriptiontable != null)
            {
                schemaTable.Columns.Add("Description");
                foreach (DataRow qq in schemaTable.Rows)
                {
                    qq["Description"] =
                        descriptiontable.Select("[Column Name]='" + qq["ColumnName"] + "'")[0]["Description"].ToString();
                }
            }
            schemaTable.AcceptChanges();
            XmlDocument doc = XmlHelper.DataTableToXml(schemaTable);
            string csharpName = GetCSharpName(baseName);
            XmlHelper.SetAttribute(doc.DocumentElement, "BaseName", csharpName);
            XmlHelper.SetAttribute(doc.DocumentElement, "BaseDbObject",
                                   baseName == csharpName ? baseName : GetDbName(baseName));
            XmlHelper.SetAttribute(doc.DocumentElement, "DefaultNamespace", CurrentSettings["DefaultNamespace"]);
            XmlHelper.SetAttribute(doc.DocumentElement, "DANamespace", CurrentSettings["DANamespace"]);
            XmlHelper.SetAttribute(doc.DocumentElement, "SourceName", CurrentSettings["SourceName"]);
            XmlHelper.SetAttribute(doc.DocumentElement, "DateTime", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            if (!File.Exists("CodePorterTempSchema.xml"))
            {
                doc.Save("CodePorterTempSchema.xml");
            }
            return doc;
        }

        public static string ExeCommand(params string[] commandTexts)
        {
            var p = new Process
                        {
                            StartInfo =
                                {
                                    FileName = "cmd.exe",
                                    UseShellExecute = false,
                                    RedirectStandardInput = true,
                                    RedirectStandardOutput = true,
                                    RedirectStandardError = true,
                                    CreateNoWindow = true
                                }
                        };
            string strOutput;
            try
            {
                p.Start();
                foreach (string cmdText in commandTexts)
                {
                    p.StandardInput.WriteLine(cmdText);
                }
                p.StandardInput.WriteLine("exit");
                strOutput = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                p.Close();
            }
            catch (Exception e)
            {
                strOutput = e.Message;
            }
            return strOutput;
        }

        public void GenerateCode(string generatorName, string baseName)
        {
            Encoding outputEncoding;
            var generatorSettings = GetSection(generatorName) as NameValueCollection;
            string typeName = generatorSettings["DataSourceType"];
            XmlDocument doc = (Activator.CreateInstance(Type.GetType(typeName)) as IDataSource).GetSchema(baseName);
            byte[] buffer = TransformToBytes(doc, generatorSettings["XslTemplate"], out outputEncoding);
            bool needMerge = NeedMergeTarget(generatorName, false);
            string filename = generatorSettings["TargetFile"].Replace("%SOURCENAME%", CurrentSettings["SourceName"]);
            string beginRegion = generatorSettings["BeginRegion"];
            string endRegion = generatorSettings["EndRegion"];
            if (!needMerge)
            {
                filename = filename.Replace("%BASENAME%", doc.DocumentElement.Attributes["BaseName"].Value);
            }
            string dir = Path.GetDirectoryName(filename);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            bool fileExists = File.Exists(filename);
            string content = outputEncoding.GetString(buffer);
            if (!needMerge)
            {
                File.WriteAllText(filename, content);
            }
            else if (!fileExists)
            {
                File.WriteAllText(filename, content);
            }
            else
            {
                string origien = File.ReadAllText(filename, outputEncoding);
                string[] beginParts = beginRegion.Split(new[] {','});
                string[] endParts = endRegion.Split(new[] {','});
                for (int i = 0; i < beginParts.Length; i++)
                {
                    int startIndex = content.IndexOf(beginParts[i]) + beginParts[i].Length;
                    int endIndex = content.IndexOf(endParts[i], startIndex);
                    string tempString = content.Substring(startIndex, endIndex - startIndex);
                    startIndex = origien.IndexOf(beginParts[i]) + beginParts[i].Length;
                    endIndex = origien.IndexOf(endParts[i], startIndex);
                    origien = origien.Insert(endIndex, tempString);
                }
                File.WriteAllText(filename, origien);
            }
        }

        private string GetCSharpName(string name)
        {
            var sb = new StringBuilder(name);
            for (int i = 0; i < name.Length; i++)
            {
                char c = name[i];
                if (((char.IsSymbol(c) || char.IsPunctuation(c)) || char.IsSeparator(c)) || char.IsWhiteSpace(c))
                {
                    sb[i] = '_';
                }
            }
            if (char.IsDigit(name[0]))
            {
                sb = sb.Insert(0, "@_");
            }
            if (Keywords.Contains(name))
            {
                sb = sb.Insert(0, '@');
            }
            return sb.ToString();
        }

        public DbAccessCommand GetDbAccessCommand()
        {
            return new SqlAccessCommand(CurrentSettings["ConnectionString"]);
        }

        private string GetDbName(string name)
        {
            return ("[" + name.Replace("]", "]]").Replace("\"", "\\\"").Replace("'", @"\'") + "]");
        }

        private List<string> GetDbObjectNames(string type, params string[] exceptNames)
        {
            var names = new List<string>();
            var sb = new StringBuilder();
            if (exceptNames != null)
            {
                foreach (string name in exceptNames)
                {
                    sb.AppendFormat(" AND [Name]!='{0}'", name);
                }
            }
            string commandText = string.Format("SELECT [Name] FROM [sysobjects] WHERE [type]='{0}'{1} ORDER BY [Name]",
                                               type, sb);
            try
            {
                DbAccessCommand dbcmd = GetDbAccessCommand();
                dbcmd.AttachAccessInfo(commandText, new DbParameter[0]);
                DataTable table = dbcmd.ExecuteDataTable();
                names.AddRange(from DataRow row in table.Rows select (string) row[0]);
            }
            catch
            {
            }
            return names;
        }

        public List<string> GetProcedureNames()
        {
            return GetDbObjectNames("P",
                                    new[]
                                        {
                                            "sp_alterdiagram", "sp_creatediagram", "sp_dropdiagram",
                                            "sp_helpdiagramdefinition", "sp_helpdiagrams", "sp_renamediagram",
                                            "sp_upgraddiagrams"
                                        });
        }

        private DataColumn GetSchemaDataColumn(ref DataTable schemaTable, string columnName, Type type)
        {
            DataColumn column = schemaTable.Columns[columnName];
            if (column == null)
            {
                column = new DataColumn(columnName, type);
                schemaTable.Columns.Add(column);
            }
            return column;
        }

        public List<string> GetTableNames()
        {
            return GetDbObjectNames("U", new[] {"sysdiagrams", "dtproperties"});
        }

        public List<string> GetViewNames()
        {
            return GetDbObjectNames("V", new[] {"sysconstraints", "syssegments"});
        }


        public bool NeedMergeTarget(string generatorName, bool autoDeleteFile)
        {
            var generatorSettings = GetSection(generatorName) as NameValueCollection;
            string filename = generatorSettings["TargetFile"].Replace("%SOURCENAME%", CurrentSettings["SourceName"]);
            bool needMerge = (!string.IsNullOrEmpty(generatorSettings["BeginRegion"]) &&
                              !string.IsNullOrEmpty(generatorSettings["EndRegion"])) &&
                             !generatorSettings["TargetFile"].Contains("%BASENAME%");
            bool append;
            bool.TryParse(generatorSettings["AppendToTarget"], out append);
            if (((!append && autoDeleteFile) && needMerge) && File.Exists(filename))
            {
                File.Delete(filename);
            }
            return needMerge;
        }

        public string RunSqlScripts()
        {
            string path = CurrentSettings["SqlScriptPath"];
            string sqlcmd = CurrentSettings["SQLCommand"];
            if (!((!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(sqlcmd)) && Directory.Exists(path)))
            {
                return string.Empty;
            }
            string[] files = Directory.GetFiles(path);
            DbAccessCommand dbcmd = Instance.GetDbAccessCommand();
            var cmdList = new List<string>();
            foreach (string file in files)
            {
                cmdList.Add(string.Format(sqlcmd, file));
            }
            return ExeCommand(cmdList.ToArray());
        }

        private byte[] TransformToBytes(XmlDocument doc, string xslTemplate, out Encoding outputEncoding)
        {
            using (var ms = new MemoryStream())
            {
                XslCompiledTransform xsl = XslCache.GetXslTransform(new[] {xslTemplate});
                outputEncoding = xsl.OutputSettings.Encoding;
                xsl.Transform(doc.CreateNavigator(), null, ms);
                ms.Flush();
                return ms.ToArray();
            }
        }

        public static object GetSection(string generatorName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream xmlres = asm.GetManifestResourceStream(CodePorterSetting.CodeSet);

            var mrc = new NameValueCollection();

            var xd = new XmlDocument();
            xd.Load(xmlres);
            XmlNode configNode = xd.DocumentElement.SelectSingleNode(generatorName);
            foreach (XmlNode node in configNode)
            {
                mrc.Add(node.Attributes[0].Value, node.Attributes[1].Value);
            }
            return mrc;
        }
    }
}