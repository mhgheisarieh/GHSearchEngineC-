using System;
using System.Collections.Generic;

namespace GHSearchEngine
{
    class Printer : IPrintable
    {
        void IPrintable.PrintResults(List<string> documents, List<Result> results)
        {
            results.ForEach((result) => Console.WriteLine(result.GetScore() + "     " + documents[result.GetIndex()]));
        }
    }
}
