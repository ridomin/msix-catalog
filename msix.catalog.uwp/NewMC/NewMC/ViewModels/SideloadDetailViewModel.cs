using System;
using System.Linq;
using msix.catalog.lib;
using NewMC.Core.Models;
using NewMC.Core.Services;
using NewMC.Helpers;

namespace NewMC.ViewModels
{
    public class SideloadDetailViewModel : Observable
    {
        private PackageInfo _item;

        public PackageInfo Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public SideloadDetailViewModel()
        {
        }

        public void Initialize(string packageId)
        {
            var data = SampleDataService.GetContentGridData();
            Item = data.First(i => i.Id == packageId);
        }
    }
}
