using System;
using System.Collections.Generic;

namespace GHSearchEngine
{
    interface IPrintable
    {
        void PrintResults(List<String> documents, List<Result> results);
    }
}
