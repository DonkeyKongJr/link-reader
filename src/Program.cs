using System;
using System.Linq;
using LinkReader.Installer;
using LinkReader.Reader;
using LinkReader.Retriever;
using Microsoft.Extensions.DependencyInjection;

namespace LinkReader
{
    static class Program
    {
        static void Main(string[] args)
        {
            var provider = ServiceInstaller.Install();

            while (true)
            {
                Console.WriteLine("Welcome to link reader!");
                Console.WriteLine("I will show you every link which is present on a html site");
                Console.WriteLine("Please provide a webiste and press [ENTER]");
                var url = Console.ReadLine();

                var retriever = RetrieverFactory.GetRetriever("Html", provider);
                var html = retriever.Retrieve(url);

                var htmlReader = ReaderFactory.GetReader("Html");
                var links = htmlReader.GetLinksFromText(html);

                links.ToList().ForEach(Console.WriteLine);
                Console.ReadKey();
            }
        }
    }
}
