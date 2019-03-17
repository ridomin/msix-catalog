using System;

using NewMC.ViewModels;

using Windows.UI.Xaml.Controls;

namespace NewMC.Views
{
    public sealed partial class SideloadPage : Page
    {
        public SideloadViewModel ViewModel { get; } = new SideloadViewModel();

        public SideloadPage()
        {
            InitializeComponent();
        }
    }
}
