using System;

using NewMC.ViewModels;

using Windows.UI.Xaml.Controls;

namespace NewMC.Views
{
    public sealed partial class SystemPage : Page
    {
        public SystemViewModel ViewModel { get; } = new SystemViewModel();

        public SystemPage()
        {
            InitializeComponent();
        }
    }
}
