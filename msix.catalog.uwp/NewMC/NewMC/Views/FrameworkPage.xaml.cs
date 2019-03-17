using System;

using NewMC.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NewMC.Views
{
    public sealed partial class FrameworkPage : Page
    {
        public FrameworkViewModel ViewModel { get; } = new FrameworkViewModel();

        public FrameworkPage()
        {
            InitializeComponent();
            Loaded += FrameworkPage_Loaded;
        }

        private async void FrameworkPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDataAsync(MasterDetailsViewControl.ViewState);
        }
    }
}
