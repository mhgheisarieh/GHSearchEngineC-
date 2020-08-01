using System;
using System.Collections.Generic;
using System.Text;

namespace GHSearchEngine
{
    class SearchEngine
    {
        private DocumentHolder documentHolder;
        private IPrintable printer;

        public SearchEngine(DocumentHolder documentHolder, IPrintable printer)
        {
            this.documentHolder = documentHolder;
            this.printer = printer;
        }

        public void Query()
        {
            while (true)
            {
                String query = Console.ReadLine();
                this.printer.PrintResults(documentHolder.GetDocuments(), new Processor().ProcessQuery(query));
            }
        }

    }
}
