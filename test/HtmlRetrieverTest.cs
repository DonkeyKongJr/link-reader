using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using FluentAssertions;
using LinkReader.Installer;
using LinkReader.Reader;
using LinkReader.Retriever;
using LinkReader.Retriever.Interfaces;
using Moq;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

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

            retriever.Should().BeOfType<HtmlRetriever>();
        }

        [Fact]
        public void ShouldThrowNotSupportedExceptionWhenReaderIsNotSupported()
        {
            Action reader = () => RetrieverFactory.GetRetriever("hans", _providers);

            reader.Should().Throw<NotSupportedException>();
        }

        [Fact]
        public void ShouldReturnEmptyStringWhenStatusCodeIsNotOk()
        {
            var moq = new Mock<IWebRequest>();
            var moqHttpWebRequest = new Mock<HttpWebRequest>();
            var moqHttpWebResponse = new Mock<HttpWebResponse>();

            moqHttpWebResponse.Setup(_ => _.StatusCode).Returns(HttpStatusCode.BadRequest);
            moqHttpWebRequest.Setup(_ => _.GetResponse()).Returns(moqHttpWebResponse.Object);
            moq.Setup(_ => _.Create(It.IsAny<string>())).Returns(moqHttpWebRequest.Object);

            var retriever = new HtmlRetriever(moq.Object);

            var result = retriever.Retrieve("test");

            result.Should().BeEmpty();
        }

        [Fact]
        public void ShouldReturnContentStringWhenStatusCodeIsOk()
        {
            var resultContent = "<html><b>I am the test</b></html>";
            var resultContentBytes = Encoding.ASCII.GetBytes(resultContent);
            var moq = new Mock<IWebRequest>();
            var moqHttpWebRequest = new Mock<HttpWebRequest>();
            var moqHttpWebResponse = new Mock<HttpWebResponse>();

            moqHttpWebResponse.Setup(_ => _.StatusCode).Returns(HttpStatusCode.OK);

            moqHttpWebResponse.Setup(_ => _.GetResponseStream()).Returns(new MemoryStream(resultContentBytes));

            moqHttpWebRequest.Setup(_ => _.GetResponse()).Returns(moqHttpWebResponse.Object);
            moq.Setup(_ => _.Create(It.IsAny<string>())).Returns(moqHttpWebRequest.Object);

            var retriever = new HtmlRetriever(moq.Object);

            var result = retriever.Retrieve("test");

            result.Should().Be(resultContent);
        }

        [Fact]
        public void ShouldReturnHttpWebRequest()
        {
            IWebRequest webRequest = _providers.GetService<IWebRequest>();

            var result = webRequest.Create("https://google.com");

            result.Should().BeOfType<HttpWebRequest>();
        }

        [Fact]
        public void ShouldThrowExceptionWhenUrlIsInvalid()
        {
            IWebRequest webRequest = _providers.GetService<IWebRequest>();

            Action result = () => webRequest.Create("abc");

            result.Should().Throw<UriFormatException>();
        }
    }
}
