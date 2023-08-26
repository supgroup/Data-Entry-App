using DataEntryApp.ApiClasses;
using System;
using System.Data.SqlClient;
 
using System.Collections.Generic;
using System.Text;
using System.IO;
//using System.Data.SqlClient;
using System.Collections.Specialized;
 
using Microsoft.SqlServer.Management.Sdk.Sfc;
namespace DataEntryApp.Classes
{
    public class Databaseclass
    {
        public static string CreateDatabase(string DataSource, string databas, string user, string pass)
        {
            string msg = "";
          //  SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");
            SqlConnection con = new SqlConnection("Data Source=" + DataSource + "; Initial Catalog=master;Integrated Security=True");

            string query = "Create Database "+ databas;
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                msg= "Database Created Successfully" ;
            }
            catch (SqlException e)
            {
             msg= "Error Generated. Details: " + e.ToString();
            }
            finally
            {
                con.Close();
               
            }
            return msg;
        }

        public static string runScript(string DataSource, string databas, string user, string pass,string scriptPath)
        {
            // string script= File.ReadAllText("D:/scrpt1.sql");
            string script = File.ReadAllText(scriptPath);
            string msg = "";
       //     SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=mydb123;Integrated Security=True");
            SqlConnection con = new SqlConnection("Data Source=" + DataSource + "; Initial Catalog=" + databas + ";Integrated Security=True");

            string query = script;
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                msg = "Tables Created Successfully";
            }
            catch (SqlException e)
            {
                msg = "Error Generated. Details: " + e.ToString();
            }
            finally
            {
                con.Close();

            }
            return msg;
        }
    }
}
