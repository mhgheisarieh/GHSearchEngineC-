using System;
using System.Collections.Generic;
using System.Text;

namespace GHSearchEngine
{
    class PreProcessedData
    {
        private static PreProcessedData ourInstance = new PreProcessedData();
        private Dictionary<String, DetailsOfWord> detailsOfWordHashMap;

        public static PreProcessedData getInstance()
        {
            return ourInstance;
        }

        private PreProcessedData()
        {
        }

        public Dictionary<String, DetailsOfWord> getDetailsOfWordHashMap()
        {
            return detailsOfWordHashMap;
        }

        public void setDetailsOfWordHashMap(Dictionary<String, DetailsOfWord> detailsOfWordHashMap)
        {
            this.detailsOfWordHashMap = detailsOfWordHashMap;
        }
    }
}
