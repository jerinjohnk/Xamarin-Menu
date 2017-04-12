using MenuForms.DataServices.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MenuForms.DataServices.Base
{
    public class RequestProvider : IRequestProvider
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public RequestProvider()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };

            _serializerSettings.Converters.Add(new StringEnumConverter());
        }

        private string CurrentTime()
        {
            DateTime now = DateTime.Now.ToLocalTime();
            return (string.Format("{0:yyyy-MM-dd H:mm:ss}", now));
        }

        public string GetQuery(string url)
        {
            string currentTime = CurrentTime();
            //var tempurl = url + ("&DeviceId=" + GlobalSettings.API_DEVICEID + "&user=" + GlobalSettings.API_USER + "&pass=" + GlobalSettings.API_PASS + "&lastActiveDate=" + currentTime);
            return "";
        }


        public async Task<TResult> GetAsync<TResult>(string uri)
        {
            using (var client = new System.Net.Http.HttpClient(new LoggingHandler(new HttpClientHandler())))
            {
                HttpResponseMessage response = await client.GetAsync(uri);

                await HandleResponse(response);

                string serialized = await response.Content.ReadAsStringAsync();
                TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

                return result;
            }
        }

        public async Task<string> GetRequest(string url)
        {
            using (var client = new System.Net.Http.HttpClient(new LoggingHandler(new HttpClientHandler())))
            {
                HttpResponseMessage response = await client.GetAsync(url);

                await HandleResponse(response);
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return result;
            }
        }





        public async Task<string> PostAsync(string Url, IList<KeyValuePair<string, string>> parameters)
        {
            using (var client = new System.Net.Http.HttpClient(new LoggingHandler(new HttpClientHandler())))
            {
                client.BaseAddress = new System.Uri(Url);
                //client.DefaultRequestHeaders.UserAgent.ParseAdd("(Android; Mobile) Chrome");
                var content = new FormUrlEncodedContent(parameters);
                var response = await client.PostAsync("", content).ConfigureAwait(false);
                //response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        public Task<TResult> PostAsync<TResult>(string uri, TResult data)
        {
            return PostAsync<TResult, TResult>(uri, data);
        }

        public async Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest data)
        {
            HttpClient httpClient = CreateHttpClient();
            string serialized = await Task.Run(() => JsonConvert.SerializeObject(data, _serializerSettings));
            HttpResponseMessage response = await httpClient.PostAsync(uri, new StringContent(serialized, Encoding.UTF8, "application/json"));

            await HandleResponse(response);

            string responseData = await response.Content.ReadAsStringAsync();

            return await Task.Run(() => JsonConvert.DeserializeObject<TResult>(responseData, _serializerSettings));
        }

        public Task<TResult> PutAsync<TResult>(string uri, TResult data)
        {
            return PutAsync<TResult, TResult>(uri, data);
        }

        public async Task<TResult> PutAsync<TRequest, TResult>(string uri, TRequest data)
        {
            HttpClient httpClient = CreateHttpClient();
            string serialized = await Task.Run(() => JsonConvert.SerializeObject(data, _serializerSettings));
            HttpResponseMessage response = await httpClient.PutAsync(uri, new StringContent(serialized, Encoding.UTF8, "application/json"));

            await HandleResponse(response);

            string responseData = await response.Content.ReadAsStringAsync();

            return await Task.Run(() => JsonConvert.DeserializeObject<TResult>(responseData, _serializerSettings));
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(content);
                }

                throw new HttpRequestException(content);
            }
        }
    }

    public class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            System.Diagnostics.Debug.WriteLine("Request:");
            System.Diagnostics.Debug.WriteLine(request.ToString());
            if (request.Content != null)
            {
                System.Diagnostics.Debug.WriteLine(await request.Content.ReadAsStringAsync());
            }
            System.Diagnostics.Debug.WriteLine("");

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            System.Diagnostics.Debug.WriteLine("Response:");
            System.Diagnostics.Debug.WriteLine(response.ToString());
            if (response.Content != null)
            {
                System.Diagnostics.Debug.WriteLine(await response.Content.ReadAsStringAsync());
            }
            System.Diagnostics.Debug.WriteLine("");

            return response;
        }
    }
}
