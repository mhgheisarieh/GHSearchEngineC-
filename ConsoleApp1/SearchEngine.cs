using System;
using System.Collections.Generic;
using System.Text;

namespace GHSearchEngine
{
    class SearchEngine
    {
        private DocumentHolder documentHolder;
        private IPrintable printer;

        SearchEngine(DocumentHolder documentHolder, IPrintable printer)
        {
            this.documentHolder = documentHolder;
            this.printer = printer;
        }

        void query()
        {
            while (true)
            {
                String query = Console.ReadLine();
                //this.printer.printResults(documentHolder.getDocuments(), new Processor().processQuery(query));
            }
        }

    }
}
