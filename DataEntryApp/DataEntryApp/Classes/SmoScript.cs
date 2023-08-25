using DataEntryApp.ApiClasses;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
//using System.Data.SqlClient;
using System.Collections.Specialized;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Sdk.Sfc;
namespace DataEntryApp.Classes
{
    public class SmoScript
    {
        public static void Main(string DataSource, string databas, string user, string pass)
        {
         //   string connectionString = "Data Source=<server>;Initial Catalog=<database>;User ID=<username>;Password=<password>";
            string connectionString = "Data Source=" + DataSource + "; Initial Catalog=" + databas + ";User ID=" + user + ";Password=" + pass;

            // تكوين معلومات الاتصال
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);

            // إنشاء كائن ServerConnection باستخدام معلومات الاتصال
            ServerConnection serverConnection = new ServerConnection(builder.DataSource);
            serverConnection.DatabaseName = builder.InitialCatalog;
            serverConnection.LoginSecure = false;
            serverConnection.Login = builder.UserID;
            serverConnection.Password = builder.Password;

            // اتصال بخادم SQL
            Server server = new Server(serverConnection);

            // استعراض الجداول في قاعدة البيانات
            List<string> tables = GetTables(server);
            string script = "";
            // توليد نص SQL لكل جدول
            foreach (string table in tables)
            {
               script += GenerateTableScript(server, table);
                //Console.WriteLine(script);
            }

            // توليد نص SQL للعلاقات بين الجداول

            string relationshipsScript = GenerateRelationshipsScript(server);
            script += relationshipsScript;
            File.WriteAllText("D:/scrpt111.sql", script);
         //   Console.WriteLine(relationshipsScript);
        }

        static List<string> GetTables(Server server)
        {
            List<string> tables = new List<string>();

            foreach (Table table in server.Databases[server.ConnectionContext.DatabaseName].Tables)
            {
                if (table.Name != "sysdiagrams")
                {
                    tables.Add(table.Name);
                }
                 
            }

            return tables;
        }

        static string GenerateTableScript(Server server, string tableName)
        {
            StringBuilder scriptBuilder = new StringBuilder();

            // استعلام عن الجدول
            Table table = server.Databases[server.ConnectionContext.DatabaseName].Tables[tableName];

            // توليد نص SQL للجدول
            ScriptingOptions options = new ScriptingOptions();
            options.IncludeHeaders = true;
            options.SchemaQualify = true;
            options.Default = true;
            options.ExtendedProperties = true;
        //  options.IncludeDatabaseContext = true;
            options.DriPrimaryKey = true;
            //options.DriForeignKeys = true;
            //options.SchemaQualifyForeignKeysReferences = true;
           
            // options.PrimaryObject = true;
            //  options.SetTargetServerVersion=ServerVersion()
            options.ScriptDrops = false;
            //options.ScriptForCreateDrop = true;
          
            options.IncludeIfNotExists = true;
            options.ScriptData = true;
            options.ScriptSchema = true;
            //options.ExcludeObjectTypes = new[] { typeof(Diagram) };
            //StringCollection tableScripts = table.Script(options);
            //
            Scripter scripter = new Scripter(server);
            scripter.Options = options;
       
          
            IEnumerable<string> tableScripts = scripter.EnumScript(new Urn[] { table.Urn });
            //  StringCollection dataScripts = scripter.EnumScript(new Urn[] { table.Urn });
            IEnumerable<string> dataScripts = scripter.EnumScript(new Urn[] { table.Urn });
            // توليد نص SQL للجدول
            foreach (string tableScript in tableScripts)
            {
                scriptBuilder.AppendLine(tableScript);
                scriptBuilder.AppendLine();
            }

            // توليد نص SQL للبيانات
            //foreach (string dataScript in dataScripts)
            //{
            //    scriptBuilder.AppendLine(dataScript);
            //    scriptBuilder.AppendLine();
            //}
            ///
            //foreach (string tableScript in tableScripts)
            //{
            //    scriptBuilder.AppendLine(tableScript);
            //    scriptBuilder.AppendLine();
            //}

            return scriptBuilder.ToString();
        }

        static string GenerateRelationshipsScript(Server server)
        {
            StringBuilder scriptBuilder = new StringBuilder();
            Database database = server.Databases[server.ConnectionContext.DatabaseName];

            // استعلام عن العلاقات
            foreach (Table table in database.Tables)
            {
                foreach (ForeignKey foreignKey in table.ForeignKeys)
                {
                    string tableName = table.Name;
                    string columnName = foreignKey.Columns[0].Name;
                    string referencedTableName = foreignKey.ReferencedTable;
                  //  string referencedColumnName = foreignKey.ReferencedColumns[0].Name;
                    string referencedColumnName = foreignKey.Columns[0].ReferencedColumn;
                    scriptBuilder.AppendLine($"ALTER TABLE {tableName}");
                    scriptBuilder.AppendLine($"ADD CONSTRAINT FK_{tableName}_{columnName}");
                    scriptBuilder.AppendLine($"FOREIGN KEY ({columnName})");
                    scriptBuilder.AppendLine($"REFERENCES {referencedTableName}({referencedColumnName});");

                    scriptBuilder.AppendLine();
                }
            }

            return scriptBuilder.ToString();
        }
    }
}
