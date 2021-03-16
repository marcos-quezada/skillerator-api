using Darnton.OpenWeather.Models;
using Darnton.OpenWeather.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Skillerator.Models;

namespace Skillerator.Controllers
{
    [ApiController]
    [Route("api/bamf-abh")]
    public class BAMFAbhProxyController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<BAMFAbhProxyController> _logger;
        static HttpClient client = new HttpClient();
        private const string SERVICE_ENDPOINT = "https://bamf-navi.bamf.de/atlas-backend/behoerden/abh";


        public BAMFAbhProxyController(IConfiguration config, ILogger<BAMFAbhProxyController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpGet]
        public async Task<AuslaenderbehoerdeData[]> Get(int cityId)
        {
            var requestMessage = new HttpRequestMessage() {
                Method = new HttpMethod("GET"),
                RequestUri = new Uri(SERVICE_ENDPOINT)
            };

            requestMessage.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            requestMessage.Headers.Add("Connection", "keep-alive");
            requestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/534.34 (KHTML, like Gecko) Qt/4.8.2");

            var response = await client.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;
            
            return await response.Content.ReadFromJsonAsync<AuslaenderbehoerdeData[]>();
        }
    }
}