using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

using Microsoft.Toolkit.Uwp.UI.Animations;
using msix.catalog.lib;
using NewMC.Core.Models;
using NewMC.Core.Services;
using NewMC.Helpers;
using NewMC.Services;
using NewMC.Views;

namespace NewMC.ViewModels
{
    public class SideloadViewModel : Observable
    {
        private ICommand _itemClickCommand;

        public ICommand ItemClickCommand => _itemClickCommand ?? (_itemClickCommand = new RelayCommand<PackageInfo>(OnItemClick));

        public ObservableCollection<PackageInfo> Source
        {
            get
            {
                // TODO WTS: Replace this with your actual data
                return new ObservableCollection<PackageInfo>
                {
                      new PackageInfo
                      {
                          Author = "yo",
                           Id = "1",
                           Version="1"
                      }
                };
            }
        }

        public SideloadViewModel()
        {
        }

        private void OnItemClick(PackageInfo clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnnimation(clickedItem);
                NavigationService.Navigate<SideloadDetailPage>(clickedItem.PFN);
            }
        }
    }
}
