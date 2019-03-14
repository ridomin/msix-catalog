using Humanizer;
using MahApps.Metro.Controls;
using msix.catalog.app.ViewModels;
using msix.catalog.lib;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;

namespace msix.catalog.app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Navigation.Navigation.Frame = new System.Windows.Controls.Frame() { NavigationUIVisibility = NavigationUIVisibility.Hidden };
            Navigation.Navigation.Frame.Navigated += SplitViewFrame_OnNavigated;

            
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Navigation.Navigation.Navigate(new Uri("Views/MainPage.xaml", UriKind.RelativeOrAbsolute));
            ((ShellViewModel)this.DataContext).StartUpTime = App.Clock.Elapsed.Humanize(2, false);
            this.Title += $"{ThisAppVersionInfo.ProductVersion} [{ThisAppVersionInfo.GetDeploymentType()}] [{ThisAppVersionInfo.GetDotNetInfo()}]";
        }

        private void SplitViewFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            this.HamburgerMenuControl.Content = e.Content;
            this.HamburgerMenuControl.SelectedItem = e.ExtraData ?? ((ShellViewModel)this.DataContext).GetItem(e.Uri);
            this.HamburgerMenuControl.SelectedOptionsItem = e.ExtraData ?? ((ShellViewModel)this.DataContext).GetOptionsItem(e.Uri);
            GoBackButton.Visibility = Navigation.Navigation.Frame.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
        }

        private void GoBack_OnClick(object sender, RoutedEventArgs e)
        {
            Navigation.Navigation.GoBack();
        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            var menuItem = e.InvokedItem as MenuItem;
            if (menuItem != null && menuItem.IsNavigation)
            {
                Navigation.Navigation.Navigate(menuItem.NavigationDestination, menuItem);
            }
        }
    }
}
