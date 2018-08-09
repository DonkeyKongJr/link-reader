using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

public class HtmlReader : IReader
{
    public IEnumerable<string> GetLinksFromUrl(string url)
    {
        var html = this.GetHtmlCodeFromUrl(url);
        return this.ExtractLinksFromString(html);
    }

    private IEnumerable<string> ExtractLinksFromString(string html)
    {
        var result = new List<string>();

        var linkregex = new Regex("<a href=\"(.*?)\">", RegexOptions.IgnoreCase);
        var mc = linkregex.Matches(html);

        foreach (Match m1 in mc)
        {
            result.Add(m1.Groups[1].ToString());
        }

        return result;
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