using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TennisBookings.Rules;

namespace TennisBookings.DependencyInjection
{
    public static class NumberRuleServiceCollectionExtensions
    {
        public static IServiceCollection AddNumberRuleService(this IServiceCollection services)
        {
            services.AddSingleton<INumberRules, CanNotBeLessThanZero>();
            services.AddSingleton<INumberRules, CanNotBeEqualToZero>();
            services.AddSingleton<INumberRules, CanNotBeGreaterThanTen>();
            // We can also use above function to avoid duplications for registered a service.
            //services.TryAddEnumerable(ServiceDescriptor.Singleton<INumberRules, CanNotBeGreaterThanTen>());

            return services;
        }
    }
}
