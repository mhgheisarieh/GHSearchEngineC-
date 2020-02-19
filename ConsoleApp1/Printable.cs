using System;
using System.Collections;
using System.Collections.Generic;

namespace GHSearchEngine
{
    interface IPrintable
    {
        void printResults(List<String> documents, List<Result> results);

    }
}
