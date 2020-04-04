using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GHSearchEngine
{
    public class DataBaseUpdater
    {
        private static DataBaseUpdater ourInstance = new DataBaseUpdater();

        public static DataBaseUpdater GetInstance()
        {
            return ourInstance;
        }

        private DataBaseUpdater()
        {
        }

        internal void InsertPrePreProcessedData(PreProcessedData preProcessedData)
        {
            SqlConnectionStringBuilder builder = BuildSqlConnectioStringBuilder();
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();
            foreach (KeyValuePair<String, DetailsOfWord> node in preProcessedData.GetDetailsOfWordHashMap())
            {
                String word = node.Key;
                //node.Value.
            }
        }

        private static SqlConnectionStringBuilder BuildSqlConnectioStringBuilder()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.UserID = "sa";
            builder.Password = "root";
            builder.InitialCatalog = "Tokens";
            builder.IntegratedSecurity = true;
            return builder;
        }
    }
}