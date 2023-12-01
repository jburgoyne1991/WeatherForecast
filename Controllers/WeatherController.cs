using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using WeatherForecast.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherForecast.Controllers
{
    public class WeatherController : Controller
    {

        public ActionResult Weather()
        {

            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public IActionResult WeatherResult()
        {
            ResultViewModel resultViewModel = new ResultViewModel();
            return View(resultViewModel);
        }

        [HttpPost]
        public async Task <IActionResult> WeatherDetail(string City)
        {

            //Assign API KEY which received from OPENWEATHERMAP.ORG  
            string appId = "9133b4eede5ea0251184093bbfb318ef";

            //API path with CITY parameter and other parameters.  
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}", City, appId);

            ResultViewModel result = new ResultViewModel();
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // Read and print the content of the response
                    string responseContent = await response.Content.ReadAsStringAsync();
                    RootObject Data = JsonConvert.DeserializeObject<RootObject>(responseContent);
                    result.Country = Data.sys.country;
                    result.City = Data.name;
                    result.Lat = Convert.ToString(Data.coord.lat);
                    result.Lon = Convert.ToString(Data.coord.lon);
                    result.Description = Data.weather[0].description;
                    result.Humidity = Convert.ToString(Data.main.humidity);
                    result.Temp = Convert.ToString(Data.main.temp);
                    result.TempFeelsLike = Convert.ToString(Data.main.feels_like);
                    result.TempMax = Convert.ToString(Data.main.temp_max);
                    result.TempMin = Convert.ToString(Data.main.temp_min);
                    result.WeatherIcon = Data.weather[0].icon;
                }

                //Return JSON string.  
                return View("WeatherResult", result);
            }
        }

    }
}

/*using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        await MakeApiCall();
    }

    static async Task MakeApiCall()
    {
        try
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Replace "YOUR_API_URL" with the actual URL of the API you want to call
                string apiUrl = "YOUR_API_URL";

                // Send the GET request
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                // Check if the request was successful (status code 200-299)
                if (response.IsSuccessStatusCode)
                {
                    // Read and print the content of the response
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("API Response:");
                    Console.WriteLine(responseContent);
                }
                else
                {
                    // Handle error responses
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions
            Console.WriteLine($"Exception: {ex.Message}");*/
