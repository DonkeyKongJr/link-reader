
using System;
using System.IO;
using System.Net;
using System.Text;
using LinkReader.Retriever.Interfaces;
using LinkReader.Retriever.WebHandler;

namespace LinkReader.Retriever
{
    public class HtmlRetriever : IRetriever
    {
        private readonly IWebRequest _webRequest;

        public HtmlRetriever(IWebRequest webRequest)
        {
            _webRequest = webRequest;
        }

        public string Retrieve(string url)
        {
            var request = (HttpWebRequest)_webRequest.Create(url);
            var result = string.Empty;

            TryRetrieve(request, out result);
            return result;
        }

        private bool TryRetrieve(HttpWebRequest request, out string sourceCode)
        {
            sourceCode = string.Empty;
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var receiveStream = response.GetResponseStream();
                        var readStream = new StreamReader(receiveStream);
                        var data = readStream.ReadToEnd();
                        readStream.Close();

                        sourceCode = data;
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}