using System;
using System.Collections.Generic;
using System.Text;

namespace GHSearchEngine
{
    class PreProcessor
    {
        public static Dictionary<String, DetailsOfWord> preProcess(List<String> docs)
        {
            Dictionary<String, DetailsOfWord> detailsOfWordHashMap = new Dictionary<String, DetailsOfWord>();
            int indexOfDoc = 0;
            foreach (String doc in docs)
            {
                preProcessDoc(doc, indexOfDoc, detailsOfWordHashMap);
                indexOfDoc++;
            }
            return detailsOfWordHashMap;
        }


        private static void preProcessDoc(String doc, int indexOfDoc, Dictionary<String, DetailsOfWord> detailsOfWordDictionary)
        {
            String[] words = Splitter.split(doc);
            int indexOfWord = 0;
            foreach (String word in words)
            {
               if (detailsOfWordDictionary.ContainsKey(word))
                {
                    setWordDetail(indexOfDoc, indexOfWord, detailsOfWordDictionary[word]);
                }
                else
                {
                    DetailsOfWord detailsOfWord = new DetailsOfWord(word);
                    setWordDetail(indexOfDoc, indexOfWord, detailsOfWord);
                    detailsOfWordDictionary.Add(word, detailsOfWord);
                }
                indexOfWord++;
            }
        }

        private static void setWordDetail(int indexOfDoc, int indexOfWord, DetailsOfWord detailsOfWord)
        {
            detailsOfWord.addWordToDocIndex(indexOfDoc);
            detailsOfWord.addIndexOfWordInDoc(indexOfDoc, indexOfWord);
        }
    }
}
