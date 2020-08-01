using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GHSearchEngine
{
    class Splitter
    {
        public static String REGEX = "[\\s.,()/\"#;'\\\\\\-:$]+";

        public static String[] Split(string stringToParse)
        {
            return Regex.Split(stringToParse, @REGEX);
        }
    }
}
