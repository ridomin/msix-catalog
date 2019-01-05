using msix.catalog.app.Mvvm;
using msix.catalog.lib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace msix.catalog.app.ViewModels
{
    public class PackagesViewModel : ViewModelBase
    {

        private static readonly ObservableCollection<PackageInfoViewModel> _sideloadedPackages = new ObservableCollection<PackageInfoViewModel>();
        private static readonly ObservableCollection<PackageInfoViewModel> _developerPackages = new ObservableCollection<PackageInfoViewModel>();
        private static readonly ObservableCollection<PackageInfoViewModel> _storePackages = new ObservableCollection<PackageInfoViewModel>();
        private static readonly ObservableCollection<PackageInfoViewModel> _systemPackages = new ObservableCollection<PackageInfoViewModel>();
        private static readonly ObservableCollection<PackageInfoViewModel> _frameworkPackages = new ObservableCollection<PackageInfoViewModel>();

        public ICollectionView SideloadedPackages { get; private set; }
        public ICollectionView DeveloperPackages { get; private set; }
        public ICollectionView StorePackages { get; private set; }
        public ICollectionView SystemPackages { get; private set; }
        public ICollectionView FrameworkPackages { get; private set; }

        public PackagesViewModel()
        {
            this.SideloadedPackages = new ListCollectionView(_sideloadedPackages);
            this.DeveloperPackages = new ListCollectionView(_developerPackages);
            this.StorePackages = new ListCollectionView(_storePackages);
            this.SystemPackages = new ListCollectionView(_systemPackages);
            this.FrameworkPackages = new ListCollectionView(_frameworkPackages);

            this.PopulatePackages();
        }

        private void PopulatePackages()
        {
            if (_systemPackages.Count < 1)
            {
                foreach (var item in ShellViewModel._cachedListOfPackages)
                {
                    if (item.IsFramework == false)
                    {
                        if (item.SignatureKind == "None")
                        {
                            _developerPackages.Add(new PackageInfoViewModel(item));
                        }
                        if (item.SignatureKind == "Developer")
                        {
                            _sideloadedPackages.Add(new PackageInfoViewModel(item));
                        }
                        if (item.SignatureKind == "Store")
                        {
                            _storePackages.Add(new PackageInfoViewModel(item));
                        }
                        if (item.SignatureKind == "System")
                        {
                            _systemPackages.Add(new PackageInfoViewModel(item));
                        }
                    }
                    else if (item.IsFramework == true)
                    {
                        _frameworkPackages.Add(new PackageInfoViewModel(item));
                    }
                    else
                    {
                        App.TelemetryClient.TrackTrace("App not categorized: " + item.PFN);
                        System.Diagnostics.Debug.WriteLine("skipping: " + item.PFN);
                    }
                }
            }
        }
    }
}

