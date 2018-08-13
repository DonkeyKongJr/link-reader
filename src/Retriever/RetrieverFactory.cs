using System;
using LinkReader.Retriever;
using LinkReader.Retriever.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LinkReader.Retriever
{
    public static class RetrieverFactory
    {
        public static IRetriever GetRetriever(string type, ServiceProvider provider)
        {
            if (type?.ToUpper() == "HTML")
            {
                var webRequestHandler = provider.GetService<IWebRequest>();
                return new HtmlRetriever(webRequestHandler);
            }
            throw new NotSupportedException();
        }
    }
}