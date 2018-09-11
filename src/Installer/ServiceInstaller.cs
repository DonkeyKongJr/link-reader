using LinkReader.Handler;
using LinkReader.Manager;
using LinkReader.Retriever.Interfaces;
using LinkReader.Retriever.WebHandler;
using LinkReader.Validator;
using Microsoft.Extensions.DependencyInjection;
using Writer;

namespace LinkReader.Installer
{
    public static class ServiceInstaller
    {
        public static ServiceProvider Install()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IWebRequest, WebRequestHandler>();
            services.AddSingleton<IValidator, UrlValidator>();
            services.AddSingleton<IHandler<string, string>, UrlProtocolHandler>();
            services.AddSingleton<LinkReaderManager>();
            services.AddSingleton<IOverwriteSamePositionWriter, OverwriteSamePositionWriter>();
            var provider = services.BuildServiceProvider();

            return provider;
        }
    }
}