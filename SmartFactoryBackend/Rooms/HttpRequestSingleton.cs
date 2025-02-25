using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using DotNetEnv;
using SmartFactoryBackend.Sensors;

namespace SmartFactoryBackend.Models
{
    public class HttpClientSingleton
    {
        private static readonly HttpClient _client;

        private HttpClientSingleton() { }
        public static HttpClient GetClient()
        {
            if _client != null
                return _client;
            else
                return _client = new HttpClient();
        }
    }
}