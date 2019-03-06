using MahApps.Metro.IconPacks;
using msix.catalog.app.Mvvm;
using msix.catalog.lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace msix.catalog.app.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {

        public static IList<PackageInfo> _cachedListOfPackages = new List<PackageInfo>();

        public string TotalPackages
        {
            get
            {
                return _cachedListOfPackages.Count().ToString();
            }
        }
        private string startupTime = string.Empty;

        public string StartUpTime
        {
            get => startupTime;
            set => SetProperty(ref startupTime, value);
        }

        public ShellViewModel()
        {
            // Build the menus
            this.Menu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.HomeSolid }, Text = "Home", NavigationDestination = new Uri("Views/MainPage.xaml", UriKind.RelativeOrAbsolute) });
            this.Menu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.ShoppingBagSolid }, Text = "Store", NavigationDestination = new Uri("Views/StoreAppsPage.xaml", UriKind.RelativeOrAbsolute) });
            this.Menu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.ColumnsSolid }, Text = "Sideload", NavigationDestination = new Uri("Views/SideloadedAppsPage.xaml", UriKind.RelativeOrAbsolute) });
            this.Menu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.UniversalAccessSolid }, Text = "Developer", NavigationDestination = new Uri("Views/DeveloperAppsPage.xaml", UriKind.RelativeOrAbsolute) });
            this.Menu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.AddressBookSolid }, Text = "Framework", NavigationDestination = new Uri("Views/FrameworkAppsPage.xaml", UriKind.RelativeOrAbsolute) });
            this.Menu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.HospitalSymbolSolid }, Text = "System", NavigationDestination = new Uri("Views/SystemAppsPage.xaml", UriKind.RelativeOrAbsolute) });

            this.OptionsMenu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.InfoCircleSolid }, Text = "About", NavigationDestination = new Uri("Views/AboutPage.xaml", UriKind.RelativeOrAbsolute) });

            AllPackages = new NotifyTaskCompletion<IList<PackageInfo>>(PackageRepository.LoadAllInstalledAppsAsync());
            AllPackages.PropertyChanged += AllPackages_PropertyChanged;
        }

        private void AllPackages_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Result")
            {
                _cachedListOfPackages = AllPackages.Result;
                base.OnPropertyChanged("TotalPackages");
                base.OnPropertyChanged("PackagesLoaded");
                base.OnPropertyChanged("PackagesLoading");
                base.OnPropertyChanged("AllPackagesStats");
                base.OnPropertyChanged("SideloadPublisherStats");
                base.OnPropertyChanged("UpdatedInTheLastDayStats");
            }
}

        public NotifyTaskCompletion<IList<PackageInfo>> AllPackages { get; private set; }

        public object GetItem(object uri)
        {
            return null == uri ? null : this.Menu.FirstOrDefault(m => m.NavigationDestination.Equals(uri));
        }

        public object GetOptionsItem(object uri)
        {
            return null == uri ? null : this.OptionsMenu.FirstOrDefault(m => m.NavigationDestination.Equals(uri));
        }

        public bool PackagesLoaded => _cachedListOfPackages.Count() > 0;

        public bool PackagesLoading => _cachedListOfPackages.Count() < 1;

        public string AllPackagesStats {
            get
            {
                int numStorePackages = _cachedListOfPackages.Where(p => p.SignatureKind == "Store" && p.IsFramework==false).Count();
                int numFrameworkPackages = _cachedListOfPackages.Where(p => p.IsFramework == true).Count();
                int numSideloadPackages = _cachedListOfPackages.Where(p => p.SignatureKind == "Developer").Count();
                int numDevPackages = _cachedListOfPackages.Where(p => p.SignatureKind == "None").Count();
                return $"{numStorePackages} apps from the Store, {numFrameworkPackages} frameworkPackages, {numSideloadPackages} sideloaded and {numDevPackages} in development.";
            }
        }

        public string SideloadPublisherStats
        {
            get
            {
                var sideloadedPackages = _cachedListOfPackages.Where(p => p.SignatureKind == "Developer");
                int numSideloadPackages = sideloadedPackages.Count();
                int numPublishers = sideloadedPackages
                                    .Select(p => p.Author)
                                    .Distinct()
                                    .Count();
                return $"{numSideloadPackages} apps from {numPublishers} different publishers";
            }
        }

        public string UpdatedInTheLastDayStats
        {
            get
            {
                int numAppsUpdatedLast24h = _cachedListOfPackages.Where(p => p.InstalledDate > DateTime.Now.AddDays(-1)).Count();
                return $"{numAppsUpdatedLast24h} apps updated in the latest 24 hours";
            }
        }

	}
}
