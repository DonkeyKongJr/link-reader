using System;
using System.Collections.Generic;
using System.Linq;
using LinkReader.Handler;
using LinkReader.Reader;
using LinkReader.Retriever;
using LinkReader.Validator;

namespace LinkReader.Manager
{
    public class LinkReaderManager
    {
        private readonly IHandler<string, string> _urlProtocolHandler;
        private readonly IValidator _urlValidator;

        public LinkReaderManager(IHandler<string, string> urlProtocolHandler, IValidator urlValidator)
        {
            _urlProtocolHandler = urlProtocolHandler;
            _urlValidator = urlValidator;
        }

        public IEnumerable<string> Run(string url, uint depth, IServiceProvider provider, string prefix = "")
        {
            var resultLinks = new List<string>();

            url = _urlProtocolHandler.Handle(url);

            if (!_urlValidator.Validate(url))
            {
                throw new ArgumentException("Your entered URL was not valid!");
            }

            var retriever = RetrieverFactory.GetRetriever("Html", provider);
            var html = retriever.Retrieve(url);

            var htmlReader = ReaderFactory.GetReader("Html");
            var links = htmlReader.GetLinksFromText(html);

            if (depth == 0)
            {
                return links;
            }

            links.ToList().ForEach(link =>
            {
                resultLinks.Add(link);

                uint decreasedDepth = depth - 1;
                var childResult = Run(link, decreasedDepth, provider, "-");
                resultLinks.AddRange(childResult);
            });

            return resultLinks;
        }
    }
}