using System;
using System.Collections.Generic;
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
                foundDocs.ForEach(docIndex=>results.Add(docIndex, new Result(docIndex, 0)));
        }

        private void SetResultsScore(String[] wordsToFind)
        {
            foreach (String word in wordsToFind)
            {
                foreach (int docIndex in results.Keys)
                {
                    int score = PreProcessedData.GetInstance().GetDetailsOfWordHashMap()[word].GetNumOfWordInDocs()[docIndex];
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
                    int firstIndex = details[words[i]].GetIndexInDoc()[docIndex];
                    int secondIndex = details[words[i + 1]].GetIndexInDoc()[docIndex];
                    if (Math.Abs(firstIndex - secondIndex) > PROXIMITY_MAX_DISTANCE)
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
            while(i < list_1.Count && j < list_2.Count)
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
                DetailsOfWord detailsOfWord = PreProcessedData.GetInstance().GetDetailsOfWordHashMap()[word];
                return new List<int>(detailsOfWord.GetNumOfWordInDocs().Keys);
            }
            else
                return null;
        }

    }
}
