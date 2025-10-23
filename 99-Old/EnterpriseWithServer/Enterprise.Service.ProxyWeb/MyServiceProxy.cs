using Enterprise.Service.Contracts;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Enterprise.Service.ProxyWeb
{
    public class MyServiceProxy : IMyService
    {
        protected readonly string _webserverurl = ConfigurationManager.AppSettings["WebApi"] ?? @"http://localhost:5000";

        protected HttpClient CreateHttpClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(_webserverurl) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        protected readonly string _api = @"api/My";

        public async Task<int> GetZero()
        {
            using (HttpClient client = CreateHttpClient())
            {
//                HttpResponseMessage response = await client.GetAsync(_api + "/" + id);
                HttpResponseMessage response = await client.GetAsync(_api);
                if (response.IsSuccessStatusCode)
                {
                    int value = await response.Content.ReadAsAsync<int>();

                    return value;
                }
            }
            return -1;
        }
    }
}
