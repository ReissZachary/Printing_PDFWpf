using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Printing_PDFWpf.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;

        public MainWindowViewModel( IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        private DelegateCommand<string> _viewForecastCommand;
        public DelegateCommand<string> ViewForecastView =>
            _viewForecastCommand ?? (_viewForecastCommand = new DelegateCommand<string>(ExecuteViewForecastView));

        void ExecuteViewForecastView(string parameter)
        {
            regionManager.RequestNavigate("ContentRegion", parameter);
        }
    }

}
