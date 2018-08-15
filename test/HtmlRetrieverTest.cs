using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using LinkReader.Installer;
using LinkReader.Reader;
using LinkReader.Retriever;
using LinkReader.Retriever.Interfaces;
using Moq;
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

            Assert.Empty(result);
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

            Assert.Equal(resultContent, result);
        }
    }
}
