using System.Net;
using System.Runtime.Serialization;
using LinkReader.Retriever.Interfaces;

namespace LinkReader.Retriever.WebHandler
{
    public class WebRequestHandler : IWebRequest
    {
        public WebRequest Create(string uri)
        {
            return WebRequest.Create(uri);
        }
    }
}