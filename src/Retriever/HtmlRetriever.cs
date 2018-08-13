
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
            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }
                var data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();

                return data;
            }

            return string.Empty;
        }
    }
}