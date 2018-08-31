using System;
using System.Linq;
using LinkReader.Reader;
using Xunit;
using FluentAssertions;
using Moq;
using LinkReader.Validator;
using LinkReader.Handler;
using LinkReader.Manager;
using LinkReader.Retriever.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LinkReaderTest
{
    public class LinkReaderManagerTest
    {

        private readonly Mock<IHandler<string, string>> _urlProtocolHandlerMock;
        private readonly Mock<IValidator> _validatorMock;

        public LinkReaderManagerTest()
        {
            _urlProtocolHandlerMock = new Mock<IHandler<string, string>>(MockBehavior.Loose);
            _validatorMock = new Mock<IValidator>(MockBehavior.Loose);
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWhenValidationFails()
        {
            _urlProtocolHandlerMock.Setup(_ => _.Handle(It.IsAny<string>())).Returns("patrickschadler.com");
            _validatorMock.Setup(_ => _.Validate(It.IsAny<string>())).Returns(false);

            var linkReaderManager = new LinkReaderManager(_urlProtocolHandlerMock.Object, _validatorMock.Object);

            Action managerRun = () => linkReaderManager.Run("", 0, null);

            managerRun.Should().Throw<ArgumentException>();
        }
    }
}
