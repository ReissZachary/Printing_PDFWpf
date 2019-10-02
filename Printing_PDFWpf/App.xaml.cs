using Printing_PDFWpf.Views;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Printing_PDFWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        //protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        //{
        //    //regionBehaviors.A
        //    base.ConfigureDefaultRegionBehaviors(regionBehaviors);
        //}
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
