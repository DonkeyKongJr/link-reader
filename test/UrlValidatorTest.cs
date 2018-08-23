using FluentAssertions;
using Xunit;
using LinkReader.Validator;
using System;
using LinkReader.Installer;
using Microsoft.Extensions.DependencyInjection;

namespace LinkReaderTest
{
    public class UrlValidatorTest
    {
        private readonly IServiceProvider _providers;
        private readonly IValidator _validator;
        public UrlValidatorTest()
        {
            _providers = ServiceInstaller.Install();
            _validator = _providers.GetService<IValidator>();
        }

        [Fact]
        public void ShouldReturnFalseIfUrlIsInvalid()
        {
            var result = _validator.Validate("not valid url");

            result.Should().BeFalse();
        }

        [Fact]
        public void ShouldReturnTrueWhenHttpUrl()
        {
            var result = _validator.Validate("http://patrickschadler.com");

            result.Should().BeTrue();
        }

        [Fact]
        public void ShouldReturnTrueWhenHttpsUrl()
        {
            var result = _validator.Validate("https://patrickschadler.com");

            result.Should().BeTrue();
        }
    }
}