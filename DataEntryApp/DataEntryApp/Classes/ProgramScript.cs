using DataEntryApp.ApiClasses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.IO;
 
namespace DataEntryApp.Classes
{
    public class ProgramScript
    {
      public  static void Main(string DataSource,string databas,string user,string pass)
        {
            try {
                string connectionString = "Data Source="+DataSource+"; Initial Catalog="+ databas + ";User ID="+ user + ";Password="+ pass;

            // اتصال بقاعدة البيانات
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // استعراض الجداول في قاعدة البيانات
                string[] tables = GetTables(connection);
                string script = "";
                // توليد نص SQL لكل جدول
                foreach (string table in tables)
                {
                      script += GenerateTableScript(connection, table);

                    // طباعة نص SQL للجدول
                 //   Console.WriteLine(script);
                }
                    // توليد نص SQL للعلاقات بين الجداول
                    string relationshipsScript = GenerateRelationshipsScript(connection);
                    script += relationshipsScript;
                    Console.WriteLine(relationshipsScript);
                    File.WriteAllText("D:/scrpt111.sql", script);
            }
            }
            catch (Exception ex)
            {
            }
        }

        static string[] GetTables(SqlConnection connection)
        {
            // استعلام لاستعراض الجداول في قاعدة البيانات
            string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';";
            SqlCommand command = new SqlCommand(query, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                // جمع أسماء الجداول
                var tables = new List<string>();

                while (reader.Read())
                {
                    string tableName = reader.GetString(0);
                    tables.Add(tableName);
                }

                return tables.ToArray();
            }
        }

        static string GenerateTableScript(SqlConnection connection, string tableName)
        {
            // استعلام لاستعراض هيكل الجدول والسجلات
            string query = $"SELECT * FROM {tableName};";
            SqlCommand command = new SqlCommand(query, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                // بناء نص SQL للجدول
                StringBuilder scriptBuilder = new StringBuilder();

                // إضافة جدول
                scriptBuilder.AppendLine($"-- توليد جدول: {tableName}");
                scriptBuilder.AppendLine("CREATE TABLE " + tableName);
                scriptBuilder.AppendLine("(");

                // إضافة أعمدة الجدول
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string columnName = reader.GetName(i);
                    string columnType = reader.GetDataTypeName(i);

                    scriptBuilder.AppendLine($"    {columnName} {columnType},");

                    // إذا كان العمود هو العمود الأخير، قم بإزالة الفاصلة
                    if (i == reader.FieldCount - 1)
                    {
                        scriptBuilder.Length -= 3;
                    }
                }

                scriptBuilder.AppendLine();
                scriptBuilder.AppendLine(");");

                scriptBuilder.AppendLine();

                // إضافة سجلات الجدول
                if (reader.HasRows)
                {
                    scriptBuilder.AppendLine($"-- إدراج سجلات الجدول: {tableName}");

                    while (reader.Read())
                    {
                        scriptBuilder.AppendLine("INSERT INTO " + tableName + " VALUES (");

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string value = reader.IsDBNull(i) ? "NULL" : "'" + reader.GetValue(i).ToString().Replace("'", "''") + "'";
                            scriptBuilder.Append($"    {value},");
                        }

                        scriptBuilder.Length -= 1; // إزالة الفاصلة الأخيرة
                        scriptBuilder.AppendLine(");");
                    }
                }

                return scriptBuilder.ToString();
            }
        }
        static string GenerateRelationshipsScript(SqlConnection connection)
        {
            StringBuilder scriptBuilder = new StringBuilder();
            scriptBuilder.AppendLine("-- توليد العلاقات");

            string query = @"
                SELECT
                    FK.TABLE_NAME AS TableName,
                    CU.COLUMN_NAME AS ColumnName,
                    PK.TABLE_NAME AS ReferencedTableName,
                    PT.COLUMN_NAME AS ReferencedColumnName
                FROM
                    INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C
                    INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME
                    INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME
                    INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME
                    INNER JOIN (
                        SELECT
                            i1.TABLE_NAME,
                            i2.COLUMN_NAME
                        FROM
                            INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1
                            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME
                        WHERE
                            i1.CONSTRAINT_TYPE = 'PRIMARY KEY'
                    ) PT ON PT.TABLE_NAME = PK.TABLE_NAME
                WHERE
                    FK.TABLE_NAME <> PK.TABLE_NAME;
            ";

            SqlCommand command = new SqlCommand(query, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string tableName = reader.GetString(0);
                    string columnName = reader.GetString(1);
                    string referencedTableName = reader.GetString(2);
                    string referencedColumnName = reader.GetString(3);

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
