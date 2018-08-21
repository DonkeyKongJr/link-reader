using LinkReader.Retriever.Interfaces;
using LinkReader.Retriever.WebHandler;
using LinkReader.Validator;
using Microsoft.Extensions.DependencyInjection;

namespace LinkReader.Installer
{
    public static class ServiceInstaller
    {
        public static ServiceProvider Install()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IWebRequest, WebRequestHandler>();
            services.AddSingleton<IValidator, UrlValidator>();
            var provider = services.BuildServiceProvider();

            return provider;
        }
    }
}