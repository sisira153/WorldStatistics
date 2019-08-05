using Microsoft.Extensions.DependencyInjection;

using WorldStatistics.Services;

namespace WorldStatistics.Configuration
{
    public static class ApplicationDomianConfig
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {         
            services.AddTransient<IDataOrganiserService, DataOrganiserService>();
        }
    }
}
