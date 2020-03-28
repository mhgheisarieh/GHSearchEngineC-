using System;
using System.Text;
using System.Data.SqlClient;


namespace GHSearchEngine
{



    class Program
    {

        static void Main(string[] args)
        {
            DocumentHolder documentHolder = new DocumentHolder(new InputSelector().selectAndGetDataSource());
            PreProcessedData.getInstance().setDetailsOfWordHashMap(PreProcessor.preProcess(documentHolder.getDocuments()));
            IPrintable printer = new Printer();
            SearchEngine searchEngine = new SearchEngine(documentHolder, printer);
            searchEngine.query();
        }
    }
}
