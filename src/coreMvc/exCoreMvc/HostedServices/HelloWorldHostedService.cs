using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace exCoreMvc.HostedServices
{
    public class HelloWorldHostedService :BackgroundService //IHostedService
    {
        private Timer _timer;
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(HelloWorld, null, 0, 10000);
            return Task.CompletedTask;
        }
        private void HelloWorld(object state)
        {
            Log.Information("Hello World.");
        }
    }
}
