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

        internal List<Result> processQuery(string query)
        {
            String[] wordsToFind = extractQueryWords(query);
            fillResults(wordsToFind);
            setResultsScore(wordsToFind);
            proximityFilter(wordsToFind);
            return getSortedResult();
        }

        private List<Result> getSortedResult()
        {
            List<Result> result = new List<Result>(results.Values);
            ResultComparator resultComparator = new ResultComparator();
            result.Sort(resultComparator);
            
            return result;
        }

        private String[] extractQueryWords(String query)
        {
            return Splitter.split(query);
        }

        private void fillResults(String[] wordsToFind)
        {
            List<int> foundDocs = findAllMatches(wordsToFind);
            if (foundDocs != null)
                foundDocs.ForEach(docIndex=>results.Add(docIndex, new Result(docIndex, 0)));
        }

        private void setResultsScore(String[] wordsToFind)
        {
            foreach (String word in wordsToFind)
            {
                foreach (int docIndex in results.Keys)
                {
                    int score = PreProcessedData.getInstance().getDetailsOfWordHashMap()[word].getNumOfWordInDocs()[docIndex];
                    results[docIndex].changeScore(score);
                }
            }
        }

        private void proximityFilter(String[] words)
        {
            List<int> toBeRemovedDocs = new List<int>();
            Dictionary<String, DetailsOfWord> details = PreProcessedData.getInstance().getDetailsOfWordHashMap();
            foreach (int docIndex in results.Keys)
            {
                for (int i = 0; i < words.Length - 1; i++)
                {
                    int firstIndex = details[words[i]].getIndexInDoc()[docIndex];
                    int secondIndex = details[words[i + 1]].getIndexInDoc()[docIndex];
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


        private static List<int> retainArray(List<int> list_1, List<int> list_2)
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

        private List<int> findAllMatches(String[] wordsToFind)
        {
            List<int> foundDocIndexes = null;
            foreach (String word in wordsToFind)
            {
                List<int> foundDocIndexesForWord = getFoundDocsIndexForWord(word);
                if (foundDocIndexesForWord != null)
                {
                    if (foundDocIndexes == null)
                        foundDocIndexes = new List<int>(foundDocIndexesForWord);
                    else
                        foundDocIndexes = retainArray(foundDocIndexes, foundDocIndexesForWord);
                }
            }
            return foundDocIndexes;
        }

        private List<int> getFoundDocsIndexForWord(String word)
        {
            if (PreProcessedData.getInstance().getDetailsOfWordHashMap().ContainsKey(word))
            {
                DetailsOfWord detailsOfWord = PreProcessedData.getInstance().getDetailsOfWordHashMap()[word];
                return new List<int>(detailsOfWord.getNumOfWordInDocs().Keys);
            }
            else
                return null;
        }

    }
}
