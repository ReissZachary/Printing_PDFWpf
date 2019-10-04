using NUnit.Framework;
using Printing_PDFWpf.Models;
using Printing_PDFWpf.Servies;
using System.Collections.ObjectModel;

namespace FilterSeviceTest
{
    public class Tests
    {
        public IFilterService filterService;

        public  ObservableCollection<ForecastModel> forecast;



        [SetUp]
        public void Setup()
        {
            filterService = new FilterService();
            forecast = getlistofForecastModel();


        }

        [Test]
        public void DataCanBeFilterdByTemp()
        {
            ObservableCollection<ForecastModel> filteredlist = filterService.FilterByTemp(forecast,"LESS_THAN",250);
            if(filteredlist.Count == 5)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void DataCanBeFilterdByType()
        {
            ObservableCollection<ForecastModel> filteredlist = filterService.FilterByType(forecast, "Clear");
            if (filteredlist.Count == 5)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        public ObservableCollection<ForecastModel> getlistofForecastModel()
        {
            var templist = new ObservableCollection<ForecastModel>();

            //building ForecastModels
            var Forecast1 = new ForecastModel();
            Forecast1.Temp= 250;
            Forecast1.Date = "2019-10-04";
            Forecast1.Time = "00:00:00";
            Forecast1.Type = "Clear";


            var Forecast2 = new ForecastModel();
            Forecast2.Temp = 270;
            Forecast2.Date = "2019-10-04";
            Forecast2.Time = "00:00:00";
            Forecast2.Type = "Clouds";

            var Forecast3 = new ForecastModel();
            Forecast3.Temp = 200;
            Forecast3.Date = "2019-10-04";
            Forecast3.Time = "00:00:00";
            Forecast3.Type = "Clouds";

            var Forecast4 = new ForecastModel();
            Forecast4.Temp = 189;
            Forecast4.Date = "2019-10-04";
            Forecast4.Time = "00:00:00";
            Forecast4.Type = "Clouds";

            var Forecast5 = new ForecastModel();
            Forecast5.Temp = 300;
            Forecast5.Date = "2019-10-04";
            Forecast5.Time = "00:00:00";
            Forecast5.Type = "Clouds";

            var Forecast6 = new ForecastModel();
            Forecast6.Temp = 248;
            Forecast6.Date = "2019-10-04";
            Forecast6.Time = "00:00:00";
            Forecast6.Type = "Clouds";

            var Forecast7 = new ForecastModel();
            Forecast7.Temp = 45;
            Forecast7.Date = "2019-10-04";
            Forecast7.Time = "00:00:00";
            Forecast7.Type = "Clear";

            var Forecast8 = new ForecastModel();
            Forecast8.Temp = 150;
            Forecast8.Date = "2019-10-04";
            Forecast8.Time = "00:00:00";
            Forecast8.Type = "Clear";

            var Forecast9 = new ForecastModel();
            Forecast9.Temp = 350;
            Forecast9.Date = "2019-10-04";
            Forecast9.Time = "00:00:00";
            Forecast9.Type = "Clear";

            var Forecast10 = new ForecastModel();
            Forecast10.Temp = 250;
            Forecast10.Date = "2019-10-04";
            Forecast10.Time = "00:00:00";
            Forecast10.Type = "Clear";


            templist.Add(Forecast1);
            templist.Add(Forecast2);
            templist.Add(Forecast3);
            templist.Add(Forecast4);
            templist.Add(Forecast5);
            templist.Add(Forecast6);
            templist.Add(Forecast7);
            templist.Add(Forecast8);
            templist.Add(Forecast9);
            templist.Add(Forecast10);

            return templist;
        }
    }
}