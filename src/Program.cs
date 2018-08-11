using System;
using System.Linq;
using LinkReader.Reader;
using LinkReader.Retriever;

namespace LinkReader
{
    protected class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Welcome to link reader!");
                Console.WriteLine("I will show you every link which is present on a html site");
                Console.WriteLine("Please provide a webiste and press [ENTER]");
                var url = Console.ReadLine();

                var retriever = RetrieverFactory.GetRetriever("Html");
                var html = retriever.Retrieve(url);

                var htmlReader = ReaderFactory.GetReader("Html");
                var links = htmlReader.GetLinksFromText(html);

                links.ToList().ForEach(Console.WriteLine);
                Console.ReadKey();
            }
        }
    }
}
