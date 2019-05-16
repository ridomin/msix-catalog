﻿using msix.catalog.app.Mvvm;
using msix.catalog.app.Telemetry;
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
        private static readonly ObservableCollection<PackageInfoViewModel> _enterprisePackages = new ObservableCollection<PackageInfoViewModel>();

        public ICollectionView SideloadedPackages { get; private set; }
        public ICollectionView DeveloperPackages { get; private set; }
        public ICollectionView StorePackages { get; private set; }
        public ICollectionView SystemPackages { get; private set; }
        public ICollectionView FrameworkPackages { get; private set; }
        public ICollectionView EnterprisePackages { get; private set; }

        public PackagesViewModel()
        {
            this.PopulatePackages();
            this.SideloadedPackages = new ListCollectionView(_sideloadedPackages.OrderByDescending(p=>p.PackageInfo.InstalledDate).ToList());
            this.DeveloperPackages = new ListCollectionView(_developerPackages.OrderByDescending(p => p.PackageInfo.InstalledDate).ToList());
            this.StorePackages = new ListCollectionView(_storePackages.OrderByDescending(p => p.PackageInfo.InstalledDate).ToList());
            this.SystemPackages = new ListCollectionView(_systemPackages.OrderByDescending(p => p.PackageInfo.InstalledDate).ToList());
            this.FrameworkPackages = new ListCollectionView(_frameworkPackages.OrderByDescending(p => p.PackageInfo.InstalledDate).ToList());
            this.EnterprisePackages = new ListCollectionView(_enterprisePackages.OrderByDescending(p => p.PackageInfo.InstalledDate).ToList());
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
                        if (item.SignatureKind == "Enterprise")
                        {
                            _enterprisePackages.Add(new PackageInfoViewModel(item));
                        }
                    }
                    else if (item.IsFramework == true)
                    {
                        _frameworkPackages.Add(new PackageInfoViewModel(item));
                    }
                    else
                    {
                        DiagnosticsClient.TrackTrace("App not categorized: " + item.PFN);
                        System.Diagnostics.Debug.WriteLine("skipping: " + item.PFN);
                    }
                }
                base.OnPropertyChanged("NumStorePackages");
                base.OnPropertyChanged("NumSideloadedPackages");
                base.OnPropertyChanged("NumDeveloperPackages");
                base.OnPropertyChanged("NumFrameworkPackages");
                base.OnPropertyChanged("NumEnterprisePackages");
                base.OnPropertyChanged("NumSystemPackages");
            }
        }

        public int NumStorePackages => _storePackages.Count;
        public int NumSideloadedPackages => _sideloadedPackages.Count;
        public int NumDeveloperPackages => _developerPackages.Count;
        public int NumFrameworkPackages => _frameworkPackages.Count;
        public int NumSystemPackages => _systemPackages.Count;
        public int NumEnterprisePackages => _enterprisePackages.Count;


    }
}

