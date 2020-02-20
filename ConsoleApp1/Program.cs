using System;

namespace GHSearchEngine
{



    class Program
    {
        static void Main(string[] args)
        {
            String strToTest = Console.ReadLine();
            String[] strings =  Splitter.split(strToTest);
            foreach (String sss in strings)
                Console.WriteLine(sss);
        }
    }
}
