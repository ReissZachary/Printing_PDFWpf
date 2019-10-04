using Newtonsoft.Json.Linq;
using Printing_PDFWpf.Models;
using Prism.Mvvm;
using Prism.Regions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Printing_PDFWpf.ViewModels
{
    public class ForecastViewModel: BindableBase,INavigationAware
    {
        static readonly HttpClient client = new HttpClient();
        public void ForecastModel()
        {


        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            var zip = navigationContext.Parameters["zip"].ToString();
            var location = await GetLocation(zip);
            List<ForecastModel> forecast = getForecast(location);
            this.Forecast = new ObservableCollection<ForecastModel>(forecast);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        private ObservableCollection<ForecastModel> forecast;

        public ObservableCollection<ForecastModel> Forecast
        {
            get { return forecast; }
            set { SetProperty(ref forecast, value); }
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

            foreach (var record in jsonForcast)
            {
                var current = new ForecastModel();
                current.Temp = record["main"]["temp"].ToObject<double>();
                current.Humidity = record["main"]["humidity"].ToObject<double>();
                current.Pressure = record["main"]["pressure"].ToObject<double>();
                current.Type = record["weather"][0]["main"].ToObject<string>();
                current.Description = record["weather"][0]["description"].ToObject<string>();
                current.Datetime = record["dt_txt"].ToString();
                current.Speed = record["wind"]["speed"].ToObject<double>();
                current.Deg = record["wind"]["deg"].ToObject<double>();
                recordlist.Add(current);
            }
            return recordlist;
        }
    }
}
