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
using skillerator.Models;

namespace InternetFridge.API.Controllers
{
    [ApiController]
    [Route("api/bamf-abh")]
    public class CurrentWeatherController : ControllerBase
    {
        private readonly IOpenWeatherService _openWeatherService;
        private readonly IConfiguration _config;
        private readonly ILogger<CurrentWeatherController> _logger;
        static HttpClient client = new HttpClient();
        private const string SERVICE_ENDPOINT = "https://bamf-navi.bamf.de/atlas-backend/behoerden/abh";


        public CurrentWeatherController(IOpenWeatherService openWeatherService, IConfiguration config, ILogger<CurrentWeatherController> logger)
        {
            _openWeatherService = openWeatherService;
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

        private CurrentWeather GetDummyCurrentWeather()
        {

            return new CurrentWeather
            {
                Coordinates = new Coordinates
                {
                    Longitude = 172.63m,
                    Latitude = -43.53m
                },
                WeatherConditions = new List<WeatherCondition>
                {
                    new WeatherCondition
                    {
                        Id = (WeatherConditionCode)803,
                        Main = "Clouds",
                        Description = "broken clouds",
                        Icon = "04n"
                    }
                },
                Base = "stations",
                MainResult = new MainResult
                {
                    Temperature = 16.07m,
                    FeelsLike = 13.93m,
                    MinTemperature = 15.56m,
                    MaxTemperature = 16.67m,
                    Pressure = 1009,
                    Humidity = 67
                },
                Visibility = 10000,
                Wind = new Wind
                {
                    Speed = 3.1m,
                    Direction = 60
                },
                Clouds = new Clouds
                {
                    Cloudiness = 53
                },
                Rain = new Precipitation
                {
                    OneHourVolume = 0m,
                    ThreeHourVolume = 0m
                },
                Snow = new Precipitation
                {
                    OneHourVolume = 0m,
                    ThreeHourVolume = 0m
                },
                Timestamp = 1585334090L,
                System = new SystemResult
                {
                    Country = "NZ",
                    Sunrise = 1585161532L,
                    Sunset = 1585204279L
                },
                TimezoneOffset = 46800,
                CityId = 2192362,
                CityName = "Christchurch"
            };
        }
    }
}