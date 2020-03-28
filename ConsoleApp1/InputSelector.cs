using System;
using System.Collections.Generic;

namespace GHSearchEngine
{
    class InputSelector
    {

        private static String FILE_NAME = "../../../English.csv";

        internal List<string> selectAndGetDataSource()
        {
            Console.WriteLine("Please Choose your input:");
            Console.WriteLine("1. CSV Excel File");
            Console.WriteLine("2. SQL Server");
            String inputType = Console.ReadLine();
            List<string> data;
            if (inputType == "1")
            {
                 data =  new CSVFileReader().readCSVFile(FILE_NAME);
            }
            else
            {
                data =  new SqlServerReader().readData();
            }
            Console.WriteLine("All done. Press any key to start GH Search Engine.");
            Console.ReadKey(true);
            Console.WriteLine("GH Search Engine Started!");
            return data;
        }
    }
}