using System;

using NewMC.ViewModels;

using Windows.UI.Xaml.Controls;

namespace NewMC.Views
{
    public sealed partial class HomePage : Page
    {
        public HomeViewModel ViewModel { get; } = new HomeViewModel();

        public HomePage()
        {
            InitializeComponent();
        }
    }
}
