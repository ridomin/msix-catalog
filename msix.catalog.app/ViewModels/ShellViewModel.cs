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
		public ICommand RefreshCommand { get; private set; }

		public static IList<PackageInfo> _cachedListOfPackages = new List<PackageInfo>();
			   

		public string TotalPackages
		{
			get
			{                
				return _cachedListOfPackages.Count().ToString();
			}
		}

		public ShellViewModel()
		{
			// Build the menus
			this.Menu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.HomeSolid }, Text = "Home", NavigationDestination = new Uri("Views/MainPage.xaml", UriKind.RelativeOrAbsolute) });
			this.Menu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.ShoppingBagSolid }, Text = "Store", NavigationDestination = new Uri("Views/StoreAppsPage.xaml", UriKind.RelativeOrAbsolute) });
			this.Menu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.ColumnsSolid }, Text = "Sideload",NavigationDestination = new Uri("Views/SideloadedAppsPage.xaml", UriKind.RelativeOrAbsolute) });
			this.Menu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.UniversalAccessSolid }, Text = "Developer", NavigationDestination = new Uri("Views/DeveloperAppsPage.xaml", UriKind.RelativeOrAbsolute) });
			this.Menu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.AddressBookSolid }, Text = "Framework", NavigationDestination = new Uri("Views/FrameworkAppsPage.xaml", UriKind.RelativeOrAbsolute) });
			this.Menu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.HospitalSymbolSolid }, Text = "System", NavigationDestination = new Uri("Views/SystemAppsPage.xaml", UriKind.RelativeOrAbsolute) });

			this.OptionsMenu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.InfoCircleSolid }, Text = "About", NavigationDestination = new Uri("Views/AboutPage.xaml", UriKind.RelativeOrAbsolute) });

			RefreshCommand = new DelegateCommand<object>(Refresh, (o) => { return true; });

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

		private void Refresh(object o)
		{
            AllPackages = new NotifyTaskCompletion<IList<PackageInfo>>(PackageRepository.LoadAllInstalledAppsAsync());
            AllPackages.PropertyChanged += AllPackages_PropertyChanged;
        }

		public bool PackagesLoaded => _cachedListOfPackages.Count() > 0;

		public bool PackagesLoading => _cachedListOfPackages.Count() < 1;

	}
}
