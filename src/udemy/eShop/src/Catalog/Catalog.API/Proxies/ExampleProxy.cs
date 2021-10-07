using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace Catalog.API.Proxies
{
    public interface IExampleProxy
    {
        Task<ExampleResponse> ExampleGetCall(ExampleRequest exampleRequest);
        Task<ExampleResponse> ExamplePutCall(ExampleRequest exampleRequest);
        Task<ExampleResponse> ExampleGetCallWithDotNet(ExampleRequest exampleRequest);
        Task<ExampleResponse> ExamplePostCallWithDotNet(ExampleRequest exampleRequest);
    }
    public class ExampleProxy : IExampleProxy
    {
        public const string AppJsonMediaType = "application/json";
        private readonly IRestClient _restClient;
        private readonly HttpClient _httpClient;
        private readonly ILogger<ExampleProxy> _logger;

        public ExampleProxy(IRestClient restClient, HttpClient httpClient, ILogger<ExampleProxy> logger)
        {
            _restClient = restClient;
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<ExampleResponse> ExampleGetCall(ExampleRequest exampleRequest)
        {
            string url = "";

            var restRequest = new RestRequest(url, Method.GET, DataFormat.Json);
            restRequest.AddQueryParameter(nameof(exampleRequest.Name), exampleRequest.Name);
            restRequest.AddQueryParameter(nameof(exampleRequest.SurName), exampleRequest.SurName);
            restRequest.AddQueryParameter(nameof(exampleRequest.Age), exampleRequest.Age?.ToString());

            IRestResponse<ExampleResponse> restResponse = await _restClient.ExecuteAsync<ExampleResponse>(restRequest);

            if (!restResponse.IsSuccessful)
            {
                if (restResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException($"{nameof(ExampleGetCall)} method get error{Environment.NewLine}" +
                                        $"Request : {JsonConvert.SerializeObject(exampleRequest)}{Environment.NewLine}" +
                                        $"Response: {restResponse.StatusCode} -- {restResponse.Content}");
                }

                throw new IntegrationException($"{nameof(ExampleGetCall)} method get error{Environment.NewLine}" +
                                               $"Request : {JsonConvert.SerializeObject(exampleRequest)}{Environment.NewLine}" +
                                               $"Response: {restResponse.StatusCode} -- {restResponse.Content}");
            }

            return JsonConvert.DeserializeObject<ExampleResponse>(restResponse.Content);
        }
        public async Task<ExampleResponse> ExamplePutCall(ExampleRequest exampleRequest)
        {

            string url = "";

            IRestRequest restRequest = new RestRequest(url, Method.PUT, DataFormat.Json);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(exampleRequest));

            IRestResponse<ExampleResponse> restResponse = await _restClient.ExecuteAsync<ExampleResponse>(restRequest);

            if (!restResponse.IsSuccessful)
            {
                throw new IntegrationException($"{nameof(exampleRequest)} operation failed{Environment.NewLine}" +
                                               $"Response => Status : {restResponse.StatusCode} - Content : {restResponse.Content}");
            }

            return JsonConvert.DeserializeObject<ExampleResponse>(restResponse.Content);
        }

        public async Task<ExampleResponse> ExampleGetCallWithDotNet(ExampleRequest exampleRequest)
        {
            string path = "";

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add(nameof(exampleRequest.Age), exampleRequest.Age?.ToString());
            queryString.Add(nameof(exampleRequest.SurName), exampleRequest.SurName);
            queryString.Add(nameof(exampleRequest.Name), exampleRequest.Name);

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{path}?{queryString}"))
            {
                using (HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage))
                {
                    string responseContent = await responseMessage.Content.ReadAsStringAsync();

                    if (!responseMessage.IsSuccessStatusCode)
                    {
                        if (responseMessage.StatusCode == HttpStatusCode.NotFound)
                        {
                            var basicErrorHttpResponse = JsonConvert.DeserializeObject<BasicErrorHttpResponse>(responseContent);
                            if (basicErrorHttpResponse != null && !string.IsNullOrEmpty(basicErrorHttpResponse.Message))
                            {
                                throw new NotFoundException(basicErrorHttpResponse.Message,
                                    new Exception($"{nameof(exampleRequest)} : {JsonConvert.SerializeObject(exampleRequest)}{Environment.NewLine}" +
                                                  $"{nameof(responseMessage)} - StatusCode : {responseMessage.StatusCode} - Content : {responseMessage.Content}"));
                            }
                        }
                        throw new IntegrationException($"{nameof(HttpStatusCode)}: {responseMessage.StatusCode}{Environment.NewLine}" +
                                                       $"{nameof(requestMessage.RequestUri)}: {requestMessage.RequestUri}{Environment.NewLine}" +
                                                       $"{nameof(responseContent)}: {responseContent}{Environment.NewLine}" +
                                                       $"{nameof(requestMessage.Headers.Authorization)}: {requestMessage.Headers.Authorization}");
                    }

                    var response = JsonConvert.DeserializeObject<ExampleResponse>(responseContent);
                    _logger.LogInformation($"{nameof(exampleRequest)} - Request : {JsonConvert.SerializeObject(exampleRequest)}{Environment.NewLine}" +
                                           $"Response : {responseContent}{Environment.NewLine}" +
                                           $"Modelled Response : {JsonConvert.SerializeObject(response)}");

                    return response;
                }
            }
        }
        public async Task<ExampleResponse> ExamplePostCallWithDotNet(ExampleRequest exampleRequest)
        {
            string url = "";

            ExampleResponse exampleResponse;

            string serializedHttpBody = JsonConvert.SerializeObject(exampleRequest);
            using (var stringContent = new StringContent(serializedHttpBody, Encoding.UTF8, AppJsonMediaType))
            {
                using (var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url) { Content = stringContent })
                {
                    using (HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage))
                    {
                        string content = await httpResponseMessage.Content.ReadAsStringAsync();

                        if (!httpResponseMessage.IsSuccessStatusCode)
                        {
                            if (httpResponseMessage.StatusCode == HttpStatusCode.Conflict)
                            {
                                var basicErrorHttpResponse = JsonConvert.DeserializeObject<BasicErrorHttpResponse>(content);
                                if (basicErrorHttpResponse != null && basicErrorHttpResponse.Message == "errorMessage")
                                {
                                    throw new ConflictException(basicErrorHttpResponse.Message);
                                }
                            }

                            if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
                            {
                                var basicErrorHttpResponse = JsonConvert.DeserializeObject<BasicErrorHttpResponse>(content);
                                if (basicErrorHttpResponse != null && !string.IsNullOrEmpty(basicErrorHttpResponse.Message))
                                {
                                    throw new ValidationException(basicErrorHttpResponse.Message,
                                                                  new Exception($"{nameof(exampleRequest)} : {JsonConvert.SerializeObject(exampleRequest)}{Environment.NewLine}" +
                                                                                $"{nameof(httpResponseMessage)} - StatusCode : {httpResponseMessage.StatusCode} - Content : {content}"));
                                }
                            }

                            throw new IntegrationException("StringResource.UnexpectedExceptionOccurs",
                                                           new Exception($"{nameof(exampleRequest)} : {JsonConvert.SerializeObject(exampleRequest)}{Environment.NewLine}" +
                                                                         $"{nameof(httpResponseMessage)} - StatusCode : {httpResponseMessage.StatusCode} - Content : {content}"));
                        }

                        exampleResponse = JsonConvert.DeserializeObject<ExampleResponse>(content);
                    }
                }
            }

            return exampleResponse;
        }

    }

    public class ExampleRequest
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public int? Age { get; set; }
    }

    public class ExampleResponse
    {
        public string Name { get; set; }
        public string SurName { get; set; }
    }
    public class BasicErrorHttpResponse
    {
        public string Message { get; set; }
        public object Data { get; set; }
    }
    public class BasicErrorResponse
    {
        public string Message { get; set; }
    }
}
