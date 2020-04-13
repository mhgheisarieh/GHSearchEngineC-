using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
            SqlConnection connection = Connector.GetInstance().GetSqlConnection();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from Documents", connection);
            textTable = new DataTable();
            dataAdapter.Fill(textTable);
            return textTable;
        }
    }
}
