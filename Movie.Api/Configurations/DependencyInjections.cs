using Microsoft.Extensions.DependencyInjection;
using Movie.Api.BusinessLogic;
using Movie.CSVHandler;

namespace Movie.Api.Configurations
{
    public static class DependencyInjections
    {
        public static IServiceCollection InjectCSVServices(this IServiceCollection services)
        {
            services.AddSingleton<ICSVService, CSVService>();

            return services;
        }

        public static IServiceCollection InjectBusinessLogicServices(this IServiceCollection services)
        {
            services
                .AddSingleton<IMetadataService, MetadataService>()
                .AddSingleton<IStatusService, StatusService>();

            return services;
        }
    }
}
