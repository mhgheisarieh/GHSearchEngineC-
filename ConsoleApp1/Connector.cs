using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace GHSearchEngine
{
    class Connector
    {
        private static Connector ourInstance = new Connector();

        private SqlConnection connection;

        public static Connector GetInstance()
        {
            return ourInstance;
        }

        private Connector()
        {
            SqlConnectionStringBuilder builder = Connector.Build();
            connection = new SqlConnection(builder.ConnectionString);
            connection.Open();
        }

        private static SqlConnectionStringBuilder Build()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.InitialCatalog = "GHSearchEngineDatabase";
            builder.IntegratedSecurity = true;
            return builder;
        }

        public SqlConnection GetSqlConnection()
        {   
            return connection;
        }
    }
}
