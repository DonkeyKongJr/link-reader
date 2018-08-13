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
            var result = htmlReader.GetLinksFromText(this.GetValidTestHtmlData());

            Assert.Equal("https://www.orf.at", result.First());
        }

        [Fact]
        public void ShouldReturnNoLinkFromInvalidATag()
        {
            var htmlReader = new HtmlReader();
            var result = htmlReader.GetLinksFromText(this.GetInvalidTestHtmlData());

            Assert.Equal(0, result.Count());
        }

        [Fact]
        public void ShouldReturnHtmlReaderFromFactory()
        {
            var reader = ReaderFactory.GetReader("html");

            Assert.IsType<HtmlReader>(reader);
        }

        [Fact]
        public void ShouldThrowNotSupportedExceptionWhenReaderIsNotSupported()
        {
            Action reader = () => ReaderFactory.GetReader("hans");

            Assert.Throws<NotSupportedException>(reader);
        }

        private string GetInvalidTestHtmlData()
        {
            return "<h1><a href=\"www.orf.at\"></a></h1>";
        }

        private string GetValidTestHtmlData()
        {
            return "<h1><a href=\"https://www.orf.at\"></a></h1>";
        }
    }
}
