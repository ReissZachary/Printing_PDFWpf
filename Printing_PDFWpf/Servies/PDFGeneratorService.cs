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
            var html = @"<h1>Hello World</h1>";
            var pdf = htmlToPDF.RenderHtmlAsPdf(html);
            pdf.Print();
        }
    }
}
