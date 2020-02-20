using System;
using System.Collections.Generic;

namespace GHSearchEngine
{
    class Printer : IPrintable
    {
        void IPrintable.printResults(List<string> documents, List<Result> results)
        {
            results.ForEach((result) => Console.WriteLine(result.getScore() + "     " + documents[result.getIndex()]));
        }
    }
}
