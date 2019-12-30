using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace exCoreMvc.Middleware
{
    public class DaisyMiddleware
    {
        private readonly RequestDelegate _next;
        public DaisyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync("Separate Daisy Middleware.");
            _next.Invoke(context);
        }
    }
}
