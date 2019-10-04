using Newtonsoft.Json.Linq;
using Printing_PDFWpf.Models;
using Printing_PDFWpf.Servies;
using Prism.Commands;
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
    public class ForecastViewModel: BindableBase, INavigationAware
    {
        private GetDataService getDataService;
        private IFilterService filterService;
        public DelegateCommand FilterByDate { get;}
        public DelegateCommand FilterByTemp { get; }
        public DelegateCommand FilterByType { get; }

        public ForecastViewModel()
        {
            getDataService = new GetDataService();
            filterService = new FilterService();

            FilterByDate = new DelegateCommand(() =>
            {
                this.filterByDate();

            });

            FilterByTemp = new DelegateCommand(() =>
            {
                this.filterByTemp();

            });

            FilterByType = new DelegateCommand(() =>
            {
                this.filterByType();

            });

        }

        public void Reset()
        {
            Forecast = FilteredForecast;
        }

        private int temp;

        public int Temp
        {
            get { return temp; }
            set { temp = value; }
        }
        private string date;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        private string time;

        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public void filterByTemp()
        {
            Forecast = filterService.FilterByTemp(FilteredForecast, "LESS_THAN",Temp);
        }

        public void filterByDate()
        {
            Forecast = filterService.FilterByDate(FilteredForecast, Date);
        }

        public void filterByType()
        {
            Forecast = filterService.FilterByType(FilteredForecast, Type);
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            var zip = navigationContext.Parameters["zip"].ToString();
            var location = await getDataService.GetLocation(zip);
            List<ForecastModel> forecast = getDataService.getForecast(location);
            this.Forecast = new ObservableCollection<ForecastModel>(forecast);
            this.FilteredForecast = this.Forecast;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext) { }

        private ObservableCollection<ForecastModel> forecast;

        public ObservableCollection<ForecastModel> Forecast
        {
            get { return forecast; }
            set { SetProperty(ref forecast, value); }
        }

        private ObservableCollection<ForecastModel> filteredForecast;

        public ObservableCollection<ForecastModel> FilteredForecast
        {
            get { return filteredForecast; }
            set { SetProperty(ref filteredForecast, value); }
        }

    }
}
