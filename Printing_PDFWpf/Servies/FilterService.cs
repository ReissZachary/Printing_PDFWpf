using Printing_PDFWpf.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printing_PDFWpf.Servies
{
    public class FilterService:IFilterService
    {

        public ObservableCollection<ForecastModel> FilterByTemp(ObservableCollection<ForecastModel> forecast, string condition, double temp)
        {
            ObservableCollection<ForecastModel> newforecastlist = new ObservableCollection<ForecastModel>();
            if (condition == "LESS_THAN")
            {
                foreach(var cast in forecast)
                {
                    if(cast.Temp < temp)
                    {
                        newforecastlist.Add(cast);
                    }
                }
            }


            return newforecastlist;
        }

        public ObservableCollection<ForecastModel> FilterByType(ObservableCollection<ForecastModel> forecast, string condition)
        {
            ObservableCollection<ForecastModel> newforecastlist = new ObservableCollection<ForecastModel>();
            
            
                foreach (var cast in forecast)
                {
                    if (cast.Type == condition)
                    {
                        newforecastlist.Add(cast);
                    }
                }
            
            return newforecastlist;
        }
    }
}
