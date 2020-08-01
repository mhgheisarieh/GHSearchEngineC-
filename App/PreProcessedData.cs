using System;
using System.Collections.Generic;
using System.Text;

namespace GHSearchEngine
{
    class PreProcessedData
    {
        private static PreProcessedData ourInstance = new PreProcessedData();
        private Dictionary<String, DetailsOfWord> detailsOfWordHashMap;

        public static PreProcessedData GetInstance()
        {
            return ourInstance;
        }

        private PreProcessedData()
        {
        }

        public Dictionary<String, DetailsOfWord> GetDetailsOfWordHashMap()
        {
            return detailsOfWordHashMap;
        }

        public void SetDetailsOfWordHashMap(Dictionary<String, DetailsOfWord> detailsOfWordHashMap)
        {
            this.detailsOfWordHashMap = detailsOfWordHashMap;
        }
    }
}
