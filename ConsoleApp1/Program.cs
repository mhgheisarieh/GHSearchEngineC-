using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace GHSearchEngine
{



    class Program
    {
        static void Main(string[] args)
        {
            DocumentHolder documentHolder = new DocumentHolder(new InputSelector().SelectAndGetDataSource());
            //PreProcessedData.GetInstance().SetDetailsOfWordHashMap(PreProcessor.PreProcess(documentHolder.GetDocuments()));
            //DataBaseUpdater.GetInstance().InsertPrePreProcessedData(PreProcessedData.GetInstance());
            SearchEngine searchEngine = new SearchEngine(documentHolder, new Printer());
            searchEngine.Query();
        }
    }
}
