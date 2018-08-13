using System.Net;
using System.Runtime.Serialization;

namespace LinkReader.Retriever.Interfaces
{
    public interface IWebRequest
    {
        WebRequest Create(string uri);
    }
}