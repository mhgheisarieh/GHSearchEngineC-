using System;
using System.Collections.Generic;
using System.Text;

namespace GHSearchEngine
{
    class PreProcessor
    {
        public static Dictionary<String, DetailsOfWord> PreProcess(List<String> docs)
        {
            Dictionary<String, DetailsOfWord> detailsOfWordHashMap = new Dictionary<String, DetailsOfWord>();
            int indexOfDoc = 0;
            foreach (String doc in docs)
            {
                PreProcessDoc(doc, indexOfDoc, detailsOfWordHashMap);
                indexOfDoc++;
            }
            return detailsOfWordHashMap;
        }


        private static void PreProcessDoc(String doc, int indexOfDoc, Dictionary<String, DetailsOfWord> detailsOfWordDictionary)
        {
            String[] words = Splitter.Split(doc.ToLower());
            int indexOfWord = 0;
            foreach (String word in words)
            {
               if (detailsOfWordDictionary.ContainsKey(word))
                {
                    SetWordDetail(indexOfDoc, indexOfWord, detailsOfWordDictionary[word]);
                }
                else
                {
                    DetailsOfWord detailsOfWord = new DetailsOfWord(word);
                    SetWordDetail(indexOfDoc, indexOfWord, detailsOfWord);
                    detailsOfWordDictionary.Add(word, detailsOfWord);
                }
                indexOfWord++;
            }
        }

        private static void SetWordDetail(int indexOfDoc, int indexOfWord, DetailsOfWord detailsOfWord)
        {
            detailsOfWord.AddWordToDocIndex(indexOfDoc);
            detailsOfWord.AddIndexOfWordInDoc(indexOfDoc, indexOfWord);
        }
    }
}
