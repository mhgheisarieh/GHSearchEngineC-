using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GHSearchEngine
{
    class Processor
    {
        public static int PROXIMITY_MAX_DISTANCE = 5;
        private Dictionary<int, Result> results = new Dictionary<int, Result>();

        public Processor()
        {
            Console.WriteLine("GH Search Engine\nSearch Results:");
        }

        internal List<Result> ProcessQuery(string query)
        {
            String[] wordsToFind = ExtractQueryWords(query);
            FillResults(wordsToFind);
            SetResultsScore(wordsToFind);
            ProximityFilter(wordsToFind);
            return GetSortedResult();
        }

        private List<Result> GetSortedResult()
        {
            List<Result> result = new List<Result>(results.Values);
            ResultComparator resultComparator = new ResultComparator();
            result.Sort(resultComparator);

            return result;
        }

        private String[] ExtractQueryWords(String query)
        {
            return Splitter.Split(query);
        }

        private void FillResults(String[] wordsToFind)
        {
            List<int> foundDocs = FindAllMatches(wordsToFind);
            if (foundDocs != null)
                foundDocs.ForEach(docIndex => results.Add(docIndex, new Result(docIndex, 0)));
        }

        private void SetResultsScore(String[] wordsToFind)
        {
            foreach (String word in wordsToFind)
            {
                foreach (int docIndex in results.Keys)
                {
                    String query = "SELECT NumOfWord FROM [GHSearchEngineDatabase].[dbo].[Tokens] WHERE Token = '" + word + "' and  DocIndex = " + (docIndex + 1).ToString();
                    SqlConnection connection = Connector.GetInstance().GetSqlConnection();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query , connection);
                    DataTable table = new DataTable();
                    dataAdapter.Fill(table);
                    List<int> scoreList = new List<int>();
                    foreach (DataRow row in table.Rows)
                        scoreList.Add(Int32.Parse(row["NumOfWord"].ToString()));
                    int score = scoreList[0];
                    results[docIndex].ChangeScore(score);
                }
            }
        }

        private void ProximityFilter(String[] words)
        {
            List<int> toBeRemovedDocs = new List<int>();
            Dictionary<String, DetailsOfWord> details = PreProcessedData.GetInstance().GetDetailsOfWordHashMap();
            foreach (int docIndex in results.Keys)
            {
                for (int i = 0; i < words.Length - 1; i++)
                {
                    List<int> firstIndexes = details[words[i]].GetIndexesInDoc()[docIndex];
                    List<int> secondIndexes = details[words[i + 1]].GetIndexesInDoc()[docIndex];
                    int minDistanceOfIndexes = int.MaxValue;
                    foreach (int j in firstIndexes)
                    {
                        foreach (int k in secondIndexes)
                        {
                            if (minDistanceOfIndexes > Math.Abs(j - k))
                                minDistanceOfIndexes = Math.Abs(j - k);
                        }
                    }
                    if (minDistanceOfIndexes > PROXIMITY_MAX_DISTANCE)
                    {
                        toBeRemovedDocs.Add(docIndex);
                    }
                }
            }
            foreach (var docIndex in toBeRemovedDocs)
            {
                results.Remove(docIndex);
            }
        }


        private static List<int> RetainArray(List<int> list_1, List<int> list_2)
        {
            list_1.Sort();
            list_2.Sort();
            int i = 0, j = 0;
            List<int> retainArray = new List<int>();
            while (i < list_1.Count && j < list_2.Count)
            {
                if (list_1[i] < list_2[j])
                    i++;
                else if (list_2[j] < list_1[i])
                    j++;
                else
                {
                    retainArray.Add(list_2[j++]);
                    i++;
                }
            }
            return retainArray;
        }

        private List<int> FindAllMatches(String[] wordsToFind)
        {
            List<int> foundDocIndexes = null;
            foreach (String word in wordsToFind)
            {
                List<int> foundDocIndexesForWord = GetFoundDocsIndexForWord(word);
                if (foundDocIndexesForWord != null)
                {
                    if (foundDocIndexes == null)
                        foundDocIndexes = new List<int>(foundDocIndexesForWord);
                    else
                        foundDocIndexes = RetainArray(foundDocIndexes, foundDocIndexesForWord);
                }
            }
            return foundDocIndexes;
        }

        private List<int> GetFoundDocsIndexForWord(String word)
        {
            if (PreProcessedData.GetInstance().GetDetailsOfWordHashMap().ContainsKey(word))
            {
                SqlConnection connection = Connector.GetInstance().GetSqlConnection();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT DocIndex FROM [GHSearchEngineDatabase].[dbo].[Tokens] WHERE Token = '" + word + "'", connection);
                DataTable textTable = new DataTable();
                dataAdapter.Fill(textTable);
                List<int> docsContainWord = new List<int>();
                foreach (DataRow row in textTable.Rows)
                    docsContainWord.Add(Int32.Parse(row["DocIndex"].ToString()) - 1);
                return docsContainWord;
            }
            else
                return null;
        }

    }
}
