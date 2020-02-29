using System;
using System.Collections.Generic;
using System.Text;

namespace GHSearchEngine
{
    /**
     * contains a word and indexes of documents which has the word as key and number of rematches in values
     */

    class DetailsOfWord
    {
        private String word;

        /**
         * A hash map to link indexes and numOfWords in doc
         */
        private Dictionary<int, int> numOfWordInDocs;
        /**
         * key: index of doc ; value: index of word in doc
         */
        private Dictionary<int, int> indexInDoc;

        public DetailsOfWord(String word)
        {
            this.word = word;
            this.numOfWordInDocs = new Dictionary<int, int>();
            this.indexInDoc = new Dictionary<int, int>();
        }

        public void addWordToDocIndex(int indexOfDoc)
        {
            if (numOfWordInDocs.ContainsKey(indexOfDoc))
                this.numOfWordInDocs[indexOfDoc] =  this.numOfWordInDocs[indexOfDoc] + 1;
            else
                this.numOfWordInDocs[indexOfDoc] = 1;
        }

        public void addIndexOfWordInDoc(int indexOfDoc, int indexOfWord)
        {
            this.indexInDoc[indexOfDoc] =  indexOfWord;
        }

        public Dictionary<int, int> getNumOfWordInDocs()
        {
            return numOfWordInDocs;
        }

        public Dictionary<int, int> getIndexInDoc()
        {
            return indexInDoc;
        }
    }
}
