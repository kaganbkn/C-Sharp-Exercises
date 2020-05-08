using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TennisBookings.Configuration;

namespace TennisBookings.Services
{
    public class ValidateOptionsService:IHostedService
    {
        private readonly IOptions<SecondValidationConfiguration> _secondValidation;
        private readonly ILogger<ValidateOptionsService> _logger;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public ValidateOptionsService(IOptions<SecondValidationConfiguration> secondValidation,
            ILogger<ValidateOptionsService> logger,
            IHostApplicationLifetime applicationLifetime)
        {
            _secondValidation = secondValidation;
            _logger = logger;
            _applicationLifetime = applicationLifetime;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _ = _secondValidation.Value.CalculateSecond;
            }
            catch (OptionsValidationException ex)
            {
                _logger.LogError("One or more options validation checks failed.");

                foreach (var failure in ex.Failures)
                {
                    _logger.LogError(failure);
                }

                _applicationLifetime.StopApplication();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
