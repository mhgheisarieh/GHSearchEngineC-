using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace GHSearchEngine
{
    class sqlServerConnector
    {
        private static void connectToServer()
        {
            try
            {
                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "localhost";   // update me
                builder.UserID = "sa";              // update me
                builder.Password = "root";      // update me
                builder.InitialCatalog = "master";

                // Connect to SQL
                Console.Write("Connecting to SQL Server ... ");
                using SqlConnection connection = new SqlConnection(builder.ConnectionString);
                connection.Open();
                Console.WriteLine("Done.");
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("All done. Press any key to finish...");
            Console.ReadKey(true);
        }
    }
}
