using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GHSearchEngine
{
    class SqlServerConnector
    {
        public void connectToServer()
        {
            DataTable textTable = extraceDataFromDatabase();
            foreach (DataRow row in textTable.Rows)
            {
                Console.WriteLine(row["\"Text\""]);
            }
            Console.WriteLine("All done. Press any key to finish...");
            Console.ReadKey(true);
        }

        private static DataTable extraceDataFromDatabase()
        {
            DataTable textTable;
            SqlConnectionStringBuilder builder = buildSqlConnectioStringBuilder();
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from English", connection);
            textTable = new DataTable();
            dataAdapter.Fill(textTable);
            return textTable;
        }

        private static SqlConnectionStringBuilder buildSqlConnectioStringBuilder()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.UserID = "sa";
            builder.Password = "root";
            builder.InitialCatalog = "English_csv_3";
            builder.IntegratedSecurity = true;
            return builder;
        }
    }
}
