using System;
using System.Collections.Generic;
using System.Text;

namespace GHSearchEngine
{
    class Processor
    {
        public static int PROXIMITY_MAX_DISTANCE = 5;
        private Dictionary<int, Result> results = new Dictionary<int, Result>();

        Processor()
        {
            Console.WriteLine("GH Search Engine\nSearch Results:");
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
            /*DetailsOfWord detailsOfWord = PreProcessedData.getInstance().getDetailsOfWordHashMap().get(word);
            if (detailsOfWord != null)
                return new List<int>(detailsOfWord.getNumOfWordInDocs().keySet());
            else*/
                return null;
        }

    }
}
