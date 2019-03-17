using System;

using NewMC.ViewModels;

using Windows.UI.Xaml.Controls;

namespace NewMC.Views
{
    public sealed partial class StorePage : Page
    {
        public StoreViewModel ViewModel { get; } = new StoreViewModel();

        public StorePage()
        {
            InitializeComponent();
        }
    }
}
