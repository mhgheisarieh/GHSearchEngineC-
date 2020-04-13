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
            SqlConnection connection = Connector.GetInstance().GetSqlConnection();
            String query;
            String indexes;
            SqlCommand command;
            foreach (KeyValuePair<String, DetailsOfWord> node in preProcessedData.GetDetailsOfWordHashMap())
            {
                String word = node.Key;
                foreach(KeyValuePair<int , List<int>> docNumAndIndexes in node.Value.GetIndexesInDoc())
                {
                    query = "INSERT INTO [GHSearchEngineDatabase].[dbo].[Tokens] (Token, DocIndex, Indexes, NumOfWord) VALUES (@Token,@DocIndex,@Indexes, @NumOfWord)";
                    indexes = "";
                    foreach (int index in node.Value.GetIndexesInDoc()[docNumAndIndexes.Key])
                    {
                        indexes = indexes + index.ToString() + ",";
                    }
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Token", word.ToLower());
                    command.Parameters.AddWithValue("@DocIndex", docNumAndIndexes.Key);
                    command.Parameters.AddWithValue("@Indexes", indexes);
                    command.Parameters.AddWithValue("@NumOfWord", node.Value.GetNumOfWordInDocs()[docNumAndIndexes.Key]);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}