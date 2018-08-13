using System;
using System.Linq;
using LinkReader.Installer;
using LinkReader.Retriever;
using Xunit;

namespace LinkReaderTest
{
    public class HtmlRetrieverTest
    {
        private readonly IServiceProvider _providers;
        public HtmlRetrieverTest()
        {
            _providers = ServiceInstaller.Install();
        }

        [Fact]
        public void ShouldReturnHtmlRetrieverFromFactory()
        {
            var retriever = RetrieverFactory.GetRetriever("html", _providers);

            Assert.IsType<HtmlRetriever>(retriever);
        }

        [Fact]
        public void ShouldThrowNotSupportedExceptionWhenReaderIsNotSupported()
        {
            Action reader = () => RetrieverFactory.GetRetriever("hans", _providers);

            Assert.Throws<NotSupportedException>(reader);
        }

    }
}
