using FluentAssertions;
using Xunit;
using LinkReader.Validator;
using System;
using LinkReader.Installer;
using Microsoft.Extensions.DependencyInjection;
using LinkReader.Handler;

namespace LinkReaderTest
{
    public class UrlProtocolHandlerTest
    {
        private readonly IServiceProvider _providers;
        private readonly IHandler<string, string> _handler;
        public UrlProtocolHandlerTest()
        {
            _providers = ServiceInstaller.Install();
            _handler = _providers.GetService<IHandler<string, string>>();
        }

        [Fact]
        public void ShouldAddHttpsToUrl1()
        {
            var url = "patrickschadler.com";

            var result = _handler.Handle(url);

            result.Should().Be("https://patrickschadler.com");
        }

        [Fact]
        public void ShouldAddHttpsToUrl2()
        {
            var url = "www.patrickschadler.com";

            var result = _handler.Handle(url);

            result.Should().Be("https://www.patrickschadler.com");
        }

        [Fact]
        public void ShouldReturnTrueWhenHttpUrl()
        {
            var url = "http://patrickschadler.com";

            var result = _handler.Handle(url);

            result.Should().Be(url);
        }
    }
}