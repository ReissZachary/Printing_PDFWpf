using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printing_PDFWpf.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;

        public MainWindowViewModel( IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        private DelegateCommand<string> _searchZipCommand;
        public DelegateCommand<string> SearchZipCommand =>
            _searchZipCommand ?? (_searchZipCommand = new DelegateCommand<string>(ExecuteSearchZipCommand));

        void ExecuteSearchZipCommand(string parameter)
        {
            regionManager.RequestNavigate("ContentRegion", parameter);
        }
    }

}
