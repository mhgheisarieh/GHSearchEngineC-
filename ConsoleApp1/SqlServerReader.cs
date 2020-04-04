using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GHSearchEngine
{
    class SqlServerReader
    {
        public List<String> ReadData()
        {
            List<String> documents = new List<string>();
            DataTable textTable = ExtractDataFromDatabase();
            foreach (DataRow row in textTable.Rows)
            {
                documents.Add(row["Text"].ToString());
            }
            return documents;
        }

        private static DataTable ExtractDataFromDatabase()
        {
            DataTable textTable;
            SqlConnectionStringBuilder builder = BuildSqlConnectioStringBuilder();
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from Documents", connection);
            textTable = new DataTable();
            dataAdapter.Fill(textTable);
            return textTable;
        }

        private static SqlConnectionStringBuilder BuildSqlConnectioStringBuilder()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.UserID = "sa";
            builder.Password = "root";
            builder.InitialCatalog = "Documents";
            builder.IntegratedSecurity = true;
            return builder;
        }
    }
}
