using System;
using msix.catalog.lib;
using NewMC.Core.Models;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NewMC.Views
{
    public sealed partial class FrameworkDetailControl : UserControl
    {
        public PackageInfo MasterMenuItem                                                                                   
        {
            get { return GetValue(MasterMenuItemProperty) as PackageInfo; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(PackageInfo), typeof(FrameworkDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));

        public FrameworkDetailControl()
        {
            InitializeComponent();
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FrameworkDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
