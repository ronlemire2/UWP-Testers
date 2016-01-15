using Ch12_PagesAndNavigation.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Ch12_PagesAndNavigation.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DataPassingAndReturningPage : Page {
        public DataPassingAndReturningPage() {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e) {
            if (e.SourcePageType.Equals(typeof(DialogPage)))
                (e.Content as DialogPage).Completed += OnDialogPageCompleted;

            base.OnNavigatedFrom(e);
        }

        private void OnGotoButtonClick(object sender, RoutedEventArgs e) {
            // Create PassData object
            PassData passData = new PassData();

            // Set the InitializeColor property from the RadioButton controls
            foreach (UIElement child in radioStack.Children)
                if ((child as RadioButton).IsChecked.Value)
                    passData.InitializeColor = (Color)(child as RadioButton).Tag;

            // Pass that object to Navigate
            this.Frame.Navigate(typeof(DialogPage), passData);
        }

        void OnDialogPageCompleted(object sender, ReturnData args) {
            // Set background from returned color
            contentGrid.Background = new SolidColorBrush(args.ReturnColor);

            // Set RadioButton for returned color
            foreach (UIElement child in radioStack.Children)
                if ((Color)(child as RadioButton).Tag == args.ReturnColor)
                    (child as RadioButton).IsChecked = true;

            (sender as DialogPage).Completed -= OnDialogPageCompleted;
        }
    }
}
