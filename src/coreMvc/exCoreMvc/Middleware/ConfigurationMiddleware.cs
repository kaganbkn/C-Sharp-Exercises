using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exCoreMvc.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace exCoreMvc.Middleware
{
    public class ConfigurationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;
        private readonly IOptions<ConfigurationData> _options; //coming from services.configure
        public ConfigurationMiddleware(RequestDelegate next,IConfiguration config,IOptions<ConfigurationData> options)
        {
            _next = next;
            _config = config;
            _options = options;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            var data1 = _config.GetSection("Data:Name").Value;
            var data2 = _options.Value.Name;
            var data3 = _config.GetSection("Data:Codes").GetChildren();
            var data4 = _options.Value.Codes;
            var data5 = _config.GetValue<int>("Data:Age",18);

            await context.Response.WriteAsync("from IOptions\n");
            foreach (var keyValuePair in data4)
            {
                await context.Response.WriteAsync(keyValuePair.Key+" : "+keyValuePair.Value + "\n");
            }
            await context.Response.WriteAsync(data2+"\n");


            await context.Response.WriteAsync("from IConfiguration\n");
            foreach (var configurationSection in data3)
            {
                await context.Response.WriteAsync(configurationSection.Key + " : " + configurationSection.Value + "\n");
            }


            await context.Response.WriteAsync(data1+"\n");
            await context.Response.WriteAsync("Age : "+data5 + "\n");
            _next.Invoke(context);
        }
    }
}
