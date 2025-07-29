using AdsPlatformsAPI.Repositories;
using AdsPlatformsAPI.Repositories.Interfaces;
using AdsPlatformsAPI.Services;
using AdsPlatformsAPI.Services.Interfaces;
using Microsoft.OpenApi.Models;

namespace AdsPlatformsAPI.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddSingleton<IAdsLocationsRepository, AdsLocationsLocalRepository>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IAdsPlatformsService, AdsPlatformsService>()
                .AddScoped<IParsingService, ParsingService>();
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddEndpointsApiExplorer()
                            .AddSwaggerGen(options =>
                            {
                                options.SwaggerDoc("AdsPlatforms", new OpenApiInfo
                                {
                                    Version = "v1",
                                    Title = "Рекламные площадки",
                                    Description = "Тестовое задание"
                                });
                            });
        }
    }
}
