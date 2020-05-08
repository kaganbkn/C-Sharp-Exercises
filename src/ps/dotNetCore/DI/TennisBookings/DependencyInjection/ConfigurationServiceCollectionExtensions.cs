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
            //config.Bind("Features",new FeaturesConfiguration()); // Another option but we need to real FeaturesConfiguration object.

            services.Configure<ValidationConfiguration>(config.GetSection("Validations"));
            services.AddSingleton<IValidationConfiguration>(sp =>
                sp.GetRequiredService<IOptions<ValidationConfiguration>>().Value); // forwarding via implementation factory
            // We can't use IOptionsSnapshot here because the we cannot consume scoped service from singleton.

            services.Configure<SecondFeaturesConfiguration>(config.GetSection("Features"));

            services.AddOptions<SecondValidationConfiguration>()
                .Bind(config.GetSection("SecondValidation"))
                .ValidateDataAnnotations();  // We can validate data but is thrown with constructor injection not startup.

            services.Configure<PersonConfiguration>(config.GetSection("Person"));
            services.AddSingleton<IValidateOptions<PersonConfiguration>, PersonConfigurationValidation>();
            // We can use both of that.
            //services.AddOptions<PersonConfiguration>()
            //    .Bind(config.GetSection("Person"))
            //    .Validate(c =>
            //    {
            //        if (!c.IsEnable)
            //            return true;
            //        if (string.IsNullOrEmpty(c.Name))
            //            return false;
            //        if (c.IsMature && c.Age < 20)
            //            return false;
            //        return true;
            //    },"Some configurations are incorrect.");


            return services;
        }
    }
}
