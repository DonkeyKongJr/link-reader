using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

public class HtmlReader : IReader
{
    public IEnumerable<string> GetLinksFromUrl(string url)
    {
        throw new System.NotImplementedException();
    }

    private string GetHtmlCodeFromUrl(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        if (response.StatusCode == HttpStatusCode.OK)
        {
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = null;

            if (response.CharacterSet == null)
            {
                readStream = new StreamReader(receiveStream);
            }
            else
            {
                readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
            }
            string data = readStream.ReadToEnd();

            response.Close();
            readStream.Close();

            return data;
        }
        return string.Empty;
    }
}