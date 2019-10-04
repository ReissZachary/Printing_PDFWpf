using Printing_PDFWpf.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printing_PDFWpf.Servies
{
    public interface IFilterService
    {
        ObservableCollection<ForecastModel> FilterByTemp(ObservableCollection<ForecastModel> forecast, string condition, double temp);
        ObservableCollection<ForecastModel> FilterByType(ObservableCollection<ForecastModel> forecast, string condition);
        ObservableCollection<ForecastModel> FilterByDate(ObservableCollection<ForecastModel> forecast, string condition);
    }
}
