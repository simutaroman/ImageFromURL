using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImageFromURL.Services
{
    public interface IHttpClientService
    {
        HttpClient Client { get; }
    }
}
