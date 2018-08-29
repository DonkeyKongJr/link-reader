using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using LinkReader.Handler;
using LinkReader.Installer;
using LinkReader.Manager;
using LinkReader.Reader;
using LinkReader.Retriever;
using LinkReader.Validator;
using Microsoft.Extensions.DependencyInjection;

namespace LinkReader
{
    [ExcludeFromCodeCoverage]
    static class Program
    {
        static void Main(string[] args)
        {
            var provider = ServiceInstaller.Install();
            var urlProtocolHandler = provider.GetService<IHandler<string, string>>();
            var urlValidator = provider.GetService<IValidator>();
            var linkReaderManager = provider.GetService<LinkReaderManager>();

            while (true)
            {
                Console.WriteLine("Welcome to link reader!");
                Console.WriteLine("I will show you every link which is present on a html site");
                Console.WriteLine("Please provide a webiste and press [ENTER]");

                var url = Console.ReadLine();

                var links = linkReaderManager.Run(url, 1, provider);

                links.ToList().ForEach(Console.WriteLine);
                Console.ReadKey();
            }
        }
    }
}
