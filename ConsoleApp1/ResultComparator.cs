using System;
using System.Collections.Generic;
using System.Text;

namespace GHSearchEngine
{
    class ResultComparator : IComparer<Result>
    {
        public int Compare(Result x, Result y)
        {

            if (x == null || y == null)
            {
                return 0;
            }

            return - x.getScore().CompareTo(y.getScore());

        }
    }

}
