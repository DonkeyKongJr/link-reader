using System;
using System.Linq;
using LinkReader.Retriever;
using Xunit;

namespace LinkReaderTest
{
    public class HtmlRetrieverTest
    {

        [Fact]
        public void shouldReturnHtmlRetrieverFromFactory()
        {
            var retriever = RetrieverFactory.GetRetriever("html");

            Assert.IsType<HtmlRetriever>(retriever);
        }

        [Fact]
        public void shouldThrowNotSupportedExceptionWhenReaderIsNotSupported()
        {
            Action reader = () => RetrieverFactory.GetRetriever("hans");

            Assert.Throws<NotSupportedException>(reader);
        }

    }
}
