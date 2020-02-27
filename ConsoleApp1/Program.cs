using System;

namespace GHSearchEngine
{



    class Program
    {

        private static String FILE_NAME = "English.csv";

        static void Main(string[] args)
        {
            DocumentHolder documentHolder = new DocumentHolder(new CSVFileReader().readCSVFile(FILE_NAME));
            //PreProcessedData.getInstance().setDetailsOfWordHashMap(PreProcessor)
        }
    }
}
