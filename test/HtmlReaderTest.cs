using System;
using System.Linq;
using LinkReader.Reader;
using Xunit;

namespace LinkReaderTest
{
    public class HtmlReaderTest
    {
        [Fact]
        public void ShouldTestXUnitTestRunnable()
        {
            Assert.Equal(4, 2 + 2);
        }

        [Fact]
        public void ShouldReturnLinkFromATag()
        {
            var htmlReader = new HtmlReader();
            var result = htmlReader.GetLinksFromText(this.GetTestHTmlData());

            Assert.Equal("www.orf.at", result.First());
        }

        private string GetTestHTmlData()
        {
            return "<h1><a href=\"www.orf.at\"></a></h1>";
        }
    }
}
