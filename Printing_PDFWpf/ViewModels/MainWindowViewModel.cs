using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Printing_PDFWpf.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using RestSharp;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;

namespace Printing_PDFWpf.ViewModels
{
    public class MainWindowViewModel : BindableDataErrorInfoBase, INotifyPropertyChanged
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
                this.ForecastList = getForecast(location);
            });
        }
        private List<ForecastModel> forecastList;

        public List<ForecastModel> ForecastList
        {
            get { return forecastList; }
            set { forecastList = value; }
        }


        private string zip;

        public string Zip
        {
            get { return zip; }
            set 
            {
                if (value.Length < 5)
                {
                    ZipError = "*Zip must contain 5 numbers";
                }
                else
                {
                    ZipError = null;
                }
                SetProperty(ref zip, value);
            }
        }

        private string zipError;
        public string ZipError
        {
            get { return zipError; }
            set
            {
                SetProperty(ref zipError, value);
                ErrorDictionary[nameof(ZipError)] = value;
                ZipErrorVisibility = value.IsLessThanFive() ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private Visibility zipErrorVisibility;
        public Visibility ZipErrorVisibility
        {
            get { return zipErrorVisibility; }
            set { SetProperty(ref zipErrorVisibility, value); }
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

        public List<ForecastModel> getForecast(List<double> latAndLong)
        {
            var client = new RestClient("https://community-open-weather-map.p.rapidapi.com/forecast?lat=" + System.Math.Round(latAndLong[0], 4) + "&lon=" + System.Math.Round(latAndLong[1], 4));
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "community-open-weather-map.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "109daa0c4fmsh59fa8ac8dd612e0p101d84jsn0d5858161f0b");
            var response = client.Execute(request).Content;
            var jsonForcast = JObject.Parse(response)["list"];

            var recordlist = new List<ForecastModel>();

            foreach(var record in jsonForcast)
            {
                var current = new ForecastModel();
                current.temp = record["main"]["temp"].ToObject<double>();
                current.humidity = record["main"]["humidity"].ToObject<double>();
                current.pressure = record["main"]["pressure"].ToObject<double>();
                current.type = record["weather"][0]["main"].ToObject<string>();
                current.description = record["weather"][0]["description"].ToObject<string>();
                current.datetime = record["dt_txt"].ToString();
                current.speed = record["wind"]["speed"].ToObject<double>();
                current.deg = record["wind"]["deg"].ToObject<double>();
                recordlist.Add(current);
            }
            return recordlist;
        }

    }

    public static class ExtensionMethod
    {
        public static bool IsLessThanFive(this string value){
            return value.Length < 5;
        }
    }
}
