using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Printing_PDFWpf.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using RestSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


        public MainWindowViewModel( IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            ViewForecast = new DelegateCommand<string>(async(uri) =>
            {
                regionManager.RequestNavigate("ContentRegion", $"{uri}?zip={zip}");

            });
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




    }

    public static class ExtensionMethod
    {
        public static bool IsLessThanFive(this string value){
            return value.Length < 5;
        }
    }
}
