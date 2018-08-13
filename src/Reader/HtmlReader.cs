using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace LinkReader.Reader
{
    public class HtmlReader : IReader
    {
        public IEnumerable<string> GetLinksFromText(string text)
        {
            return this.ExtractLinksFromString(text);
        }

        private IEnumerable<string> ExtractLinksFromString(string html)
        {
            var result = new List<string>();

            var linkregex = new Regex("<a href=\"(.*?)\">", RegexOptions.IgnoreCase);
            var mc = linkregex.Matches(html);

            foreach (Match m1 in mc)
            {
                var href = m1.Groups[1].ToString();
                if (href.StartsWith("http"))
                {
                    result.Add(this.CheckResultUrl(href));
                }
            }

            return result;
        }

        private string CheckResultUrl(string url)
        {
            if (url.Contains('"'))
            {
                url = url.Split('"')[0];
            }

            return url;
        }
    }
}