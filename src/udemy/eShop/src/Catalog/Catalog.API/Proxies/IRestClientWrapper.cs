using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace Catalog.API.Proxies
{
    public interface IRestClientWrapper
    {
        void SetBaseUrl(Uri baseUrl);

        IRestResponse Execute(IRestRequest restRequest);
        IRestResponse<T> Execute<T>(IRestRequest restRequest) where T : new();

        Task<IRestResponse> ExecuteTaskAsync(IRestRequest restRequest);
        Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest restRequest) where T : new();
    }
    public class RestClientWrapper
    {
        private readonly IRestClient _restClient;
        private readonly ILogger<IRestClientWrapper> _logger;

        public RestClientWrapper(IRestClient restClient, ILogger<IRestClientWrapper> logger)
        {
            _restClient = restClient;
            _logger = logger;
        }

        public void SetBaseUrl(Uri baseUrl)
        {
            _restClient.BaseUrl = baseUrl;
        }

        public IRestResponse Execute(IRestRequest restRequest)
        {
            IRestResponse response = _restClient.Execute(restRequest);
            Log(restRequest, response);
            return response;
        }

        public IRestResponse<T> Execute<T>(IRestRequest restRequest) where T : new()
        {
            IRestResponse<T> response = _restClient.Execute<T>(restRequest);
            Log(restRequest, response);
            return response;
        }

        public async Task<IRestResponse> ExecuteTaskAsync(IRestRequest restRequest)
        {
            IRestResponse response = await _restClient.ExecuteTaskAsync(restRequest);
            Log(restRequest, response);
            return response;
        }

        public async Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest restRequest) where T : new()
        {
            IRestResponse<T> response = await _restClient.ExecuteTaskAsync<T>(restRequest);
            Log(restRequest, response);
            return response;
        }

        private void Log(IRestRequest restRequest, IRestResponse restResponse)
        {
            _logger.LogDebug($"Request = {JsonConvert.SerializeObject(restRequest.Parameters)}{Environment.NewLine}" +
                             $"Response = StatusCode : {restResponse.StatusCode} - Content : {restResponse.Content}");
        }
    }
}
