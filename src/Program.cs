using System;
using System.Linq;
using LinkReader.Reader;

namespace LinkReader
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Welcome to link reader!");
                Console.WriteLine("I will show you every link which is present on a html site");
                Console.WriteLine("Please provide a webiste and press [ENTER]");
                var url = Console.ReadLine();

                var htmlReader = ReaderFactory.GetReader("Html");
                var links = htmlReader.GetLinksFromUrl(url);

                links.ToList().ForEach(Console.WriteLine);
                Console.ReadKey();
            }
        }
    }
}
