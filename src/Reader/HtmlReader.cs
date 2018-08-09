using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace LinkReader.Reader
{
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
            var retriever = RetrieverFactory.GetRetriever("html");
            return retriever.Retrieve(url);
        }
    }
}