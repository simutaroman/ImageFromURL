using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImageFromURL.Services
{
    public class HttpClientService : IHttpClientService
    {
        private static HttpClient client = new HttpClient();
        public HttpClient Client
        {
            get
            {
                return client;
            }
        }
    }
}
