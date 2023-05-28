using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WPFLayer.WebApi
{
    public static class HttpClientSingleton
    {
        private static readonly Lazy<HttpClient> _httpClientLazy = new Lazy<HttpClient>(() =>
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.coingecko.com/api/v3/")
            };
            return httpClient;
        });

        public static HttpClient Instance => _httpClientLazy.Value;
    }
}
