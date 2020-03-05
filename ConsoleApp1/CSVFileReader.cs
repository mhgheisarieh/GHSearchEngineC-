using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GHSearchEngine
{
    class CSVFileReader
    {
        public List<String> readCSVFile(String filename)
        {
            List<String> documents = new List<string>();
            string[] lines = System.IO.File.ReadAllLines(@filename);
            foreach (String line in lines)
            {
                String now_line = Regex.Split(line , "\",\"")[1];
                now_line = now_line.Substring(0 , now_line.Length - 1);
                documents.Add(now_line);
            }
            return documents;
        }
    }
}
