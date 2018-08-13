
using System.IO;
using System.Net;
using System.Text;
using LinkReader.Retriever.WebHandler;

namespace LinkReader.Retriever
{
    public class HtmlRetriever : IRetriever
    {
        public string Retrieve(string url)
        {
            var webRequestHandler = new WebRequestHandler();

            var request = (HttpWebRequest)webRequestHandler.Create(url);
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