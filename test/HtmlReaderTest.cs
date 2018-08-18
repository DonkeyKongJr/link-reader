using System;
using System.Linq;
using LinkReader.Reader;
using Xunit;
using FluentAssertions;

namespace LinkReaderTest
{
    public class HtmlReaderTest
    {
        [Fact]
        public void ShouldTestXUnitTestRunnable()
        {
            (2 + 2).Should().Be(4);
        }

        [Fact]
        public void ShouldReturnLinkFromATag()
        {
            var htmlReader = new HtmlReader();
            var result = htmlReader.GetLinksFromText(this.GetValidTestHtmlData());

            result.First().Should().Be("https://www.orf.at");
        }

        [Fact]
        public void ShouldReturnNoLinkFromInvalidATag()
        {
            var htmlReader = new HtmlReader();
            var result = htmlReader.GetLinksFromText(this.GetInvalidTestHtmlData());

            result.Count().Should().Be(0);
        }

        [Fact]
        public void ShouldReturnHtmlReaderFromFactory()
        {
            var reader = ReaderFactory.GetReader("html");

            reader.Should().BeOfType<HtmlReader>();
        }

        [Fact]
        public void ShouldThrowNotSupportedExceptionWhenReaderIsNotSupported()
        {
            Action reader = () => ReaderFactory.GetReader("hans");

            reader.Should().Throw<NotSupportedException>();
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
