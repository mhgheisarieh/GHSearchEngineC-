using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace GHSearchEngine
{



    class Program
    {
        private static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if (c != '\'')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        private static DataTable ExtractDataFromDatabase()
        {
            DataTable textTable;
            SqlConnectionStringBuilder builder = BuildSqlConnectioStringBuilder();
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from English", connection);
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

        static void Main(string[] args)
        {
            /* SqlConnectionStringBuilder builder = BuildSqlConnectioStringBuilder();
             SqlConnection connection = new SqlConnection(builder.ConnectionString);
             String commandStr;
             String Title;
             String Text;
             SqlCommand command;
             SqlDataReader reader;
             SqlDataAdapter adapter = new SqlDataAdapter();
             connection.Open();
             for (int i = 1; i <= 1000; i++)
             {
                 commandStr = "SELECT Title ,Text ,ID FROM English WHERE ID = " + i.ToString();
                 command = new SqlCommand(commandStr, connection);
                 reader = command.ExecuteReader();
                 reader.Read();
                 Title = reader.GetValue(0).ToString().Remove(0, 1);
                 Text = reader.GetValue(1).ToString();
                 Text = Text.Substring(0, Text.Length - 1);
                 reader.Close();
                 Text = RemoveSpecialCharacters(Text);
                 Title = RemoveSpecialCharacters(Title);
                 Console.WriteLine(Text);
                 commandStr = "UPDATE  [Documents].[dbo].[English] SET Title = '" + Title + "', Text = '" + Text + "' WHERE ID = " + i.ToString() + ";";
                 command = new SqlCommand(commandStr, connection);
                 adapter.UpdateCommand = new SqlCommand(commandStr, connection);
                 adapter.UpdateCommand.ExecuteNonQuery();
                 command.Dispose();

             }
             connection.Close();*/
            DocumentHolder documentHolder = new DocumentHolder(new InputSelector().SelectAndGetDataSource());
            PreProcessedData.GetInstance().SetDetailsOfWordHashMap(PreProcessor.PreProcess(documentHolder.GetDocuments()));
            //DataBaseUpdater.GetInstance().InsertPrePreProcessedData(PreProcessedData.GetInstance());
            SearchEngine searchEngine = new SearchEngine(documentHolder, new Printer());
            searchEngine.Query();
        }
    }
}
