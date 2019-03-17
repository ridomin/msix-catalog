using System;

using Microsoft.Toolkit.Uwp.UI.Animations;

using NewMC.Services;
using NewMC.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace NewMC.Views
{
    public sealed partial class SideloadDetailPage : Page
    {
        public SideloadDetailViewModel ViewModel { get; } = new SideloadDetailViewModel();

        public SideloadDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is string orderId)
            {
                ViewModel.Initialize(orderId);
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnnimation(ViewModel.Item);
            }
        }
    }
}
