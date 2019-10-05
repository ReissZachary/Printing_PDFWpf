using IronPdf;
using Printing_PDFWpf.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printing_PDFWpf.Servies
{
    public class PDFGeneratorService
    {
        public void GeneratePDF(ObservableCollection<ForecastModel> forecastModel)
        {
            var htmlToPDF = new HtmlToPdf();
            var html = @"<h1>Weather Service Pro</h1>";
             html += "<table>";
             html += "<th>temp</th>";
             html += "<th>Pressure</th>";
             html += "<th>Humidity</th>";
             html += "<th>Wind Speed</th>";
             html += "<th>Wind Direction</th>";
             html += "<th>Weather Type</th>";
            html += "<th>Discription</th>";
            html += "<th>Date</th>";
            html += "<th>Time</th>";
            foreach (var cast in forecastModel)
            {
                html += "<tr>";
                html += "<td>";
                html += cast.Temp.ToString();
                html += "</td>";
                html += "<td>";
                html += cast.Pressure.ToString();
                html += "</td>";
                html += "<td>";
                html += cast.Humidity.ToString();
                html += "</td>";
                html += "<td>";
                html += cast.Speed.ToString();
                html += "</td>";
                html += "<td>";
                html += cast.Deg.ToString();
                html += "</td>";
                html += "<td>";
                html += cast.Type;
                html += "</td>";
                html += "<td>";
                html += cast.Description;
                html += "</td>";
                html += "<td>";
                html += cast.Date;
                html += "</td>";
                html += "<td>";
                html += cast.Time;
                html += "</td>";
                html += "</tr>";
            }
            html += "</table>";





            var pdf = htmlToPDF.RenderHtmlAsPdf(html);
            pdf.Print();
        }
    }
}
