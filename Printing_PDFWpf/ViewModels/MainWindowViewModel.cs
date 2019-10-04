using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Printing_PDFWpf.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using RestSharp;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Printing_PDFWpf.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;

        public DelegateCommand<string> ViewForecast { get; }
        static readonly HttpClient client = new HttpClient();

        public MainWindowViewModel( IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            ViewForecast = new DelegateCommand<string>(async(uri) =>
            {
                regionManager.RequestNavigate("ContentRegion", uri);

                var location = await GetLocation(this.Zip);
                var forecast = getForecast(location);
            });
        }

        private string zip;

        public string Zip
        {
            get { return zip; }
            set { zip = value; }
        }


        public async Task<List<double>> GetLocation(string zip)
        {
            var url = "http://dev.virtualearth.net/REST/v1/Locations/US/" + zip + "/1%20Microsoft%20Way?o=json&key=AsSjGkhZVfQ7oQw74JxKuxXJhswXesM_O4BEzLz-og-cIpdjIJ1wC2TLWSa6TL0B";
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var jsonLocation = JObject.Parse(responseBody)["resourceSets"][0]["resources"][0]["geocodePoints"][0]["coordinates"];
            var lat = jsonLocation[0].ToObject<double>();
            var longitude = jsonLocation[1].ToObject<double>();
            List<double> latAndLong = new List<double>();
            latAndLong.Add(lat);
            latAndLong.Add(longitude);
            return latAndLong;
        }

        public async Task getForecast(List<double> latAndLong)
        {
            var client = new RestClient("https://community-open-weather-map.p.rapidapi.com/forecast?lat=" + System.Math.Round(latAndLong[0], 4) + "&lon=" + System.Math.Round(latAndLong[1], 4));
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "community-open-weather-map.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "109daa0c4fmsh59fa8ac8dd612e0p101d84jsn0d5858161f0b");
            IRestResponse response = client.Execute(request);


        }

    }

}
