using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Toolkit.Uwp.UI.Controls;
using msix.catalog.lib;
using NewMC.Core.Models;
using NewMC.Core.Services;
using NewMC.Helpers;

namespace NewMC.ViewModels
{
    public class FrameworkViewModel : Observable
    {
        private PackageInfo _selected;
        private static IList<PackageInfo> _allPackages = null;
        public PackageInfo Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        public ObservableCollection<PackageInfo> Packages { get; private set; } = new ObservableCollection<PackageInfo>();

        public FrameworkViewModel()
        {
        }

        public async Task LoadDataAsync(MasterDetailsViewState viewState)
        {

            Packages.Clear();

            if (_allPackages==null)
            {
                _allPackages = await msix.catalog.lib.PackageRepository.LoadAllInstalledAppsAsync();
            }
                       
            foreach (var item in _allPackages.Where(p=>p.SignatureKind=="Developer"))
            {
                Packages.Add(item);
            }

            if (viewState == MasterDetailsViewState.Both)
            {
                Selected = Packages.First();
            }
        }
    }
}
