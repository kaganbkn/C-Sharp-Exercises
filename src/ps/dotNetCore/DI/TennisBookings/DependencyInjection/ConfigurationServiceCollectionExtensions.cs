using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TennisBookings.Configuration;

namespace TennisBookings.DependencyInjection
{
    public static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<FeaturesConfiguration>(config.GetSection("Features"));

            services.Configure<ValidationConfiguration>(config.GetSection("Validations"));
            services.AddSingleton<IValidationConfiguration>(sp =>
                sp.GetRequiredService<IOptions<ValidationConfiguration>>().Value); // forwarding via implementation factory
            return services;
        }
    }
}
