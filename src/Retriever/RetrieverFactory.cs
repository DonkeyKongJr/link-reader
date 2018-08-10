using System;
using LinkReader.Retriever;

namespace LinkReader.Retriever
{
    public static class RetrieverFactory
    {
        public static IRetriever GetRetriever(string type)
        {
            if (type?.ToUpper() == "HTML")
            {
                return new HtmlRetriever();
            }
            throw new NotSupportedException();
        }
    }
}