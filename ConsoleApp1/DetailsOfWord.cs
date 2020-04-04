using System;
using System.Collections.Generic;
using System.Text;

namespace GHSearchEngine
{
    /// <summary>
    /// contains a word and indexes of documents which has the word as key and number of rematches in values
    /// </summary>

    class DetailsOfWord
    {
        private String word;

        /// <value>
        ///  A hash map to link indexes and numOfWords in doc
        /// </value>
        private Dictionary<int, int> NumOfWordInDocs;
        /// <value>
        /// key: index of doc ; value: index of word in doc
        /// </value>
        private Dictionary<int, List<int>> IndexInDoc;

        public DetailsOfWord(String word)
        {
            this.word = word;
            this.NumOfWordInDocs = new Dictionary<int, int>();
            this.IndexInDoc = new Dictionary<int, List<int>>();
        }

        public void AddWordToDocIndex(int indexOfDoc)
        {
            if (NumOfWordInDocs.ContainsKey(indexOfDoc))
                this.NumOfWordInDocs[indexOfDoc] = this.NumOfWordInDocs[indexOfDoc] + 1;
            else
                this.NumOfWordInDocs[indexOfDoc] = 1;
        }

        public void AddIndexOfWordInDoc(int indexOfDoc, int indexOfWord)
        {
            if (IndexInDoc.ContainsKey(indexOfDoc))
            {
                this.IndexInDoc[indexOfDoc].Add(indexOfWord);
            }
            else
            {
                IndexInDoc[indexOfDoc] = new List<int>();
                IndexInDoc[indexOfDoc].Add(indexOfWord);
            }
        }

        public Dictionary<int, int> GetNumOfWordInDocs()
        {
            return NumOfWordInDocs;
        }

        public Dictionary<int, List<int>> GetIndexesInDoc()
        {
            return IndexInDoc;
        }
    }
}
