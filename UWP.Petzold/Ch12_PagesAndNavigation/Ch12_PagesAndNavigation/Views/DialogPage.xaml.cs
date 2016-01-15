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
    public sealed partial class DialogPage : Page {
        public event EventHandler<ReturnData> Completed;

        public DialogPage() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            // Get the object passed as the second argument to Navigate
            PassData passData = e.Parameter as PassData;

            // Use that to initialize the RadioButton controls
            foreach (UIElement child in radioStack.Children)
                if ((Color)(child as RadioButton).Tag == passData.InitializeColor)
                    (child as RadioButton).IsChecked = true;

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e) {
            if (Completed != null) {
                // Create ReturnData object
                ReturnData returnData = new ReturnData();

                // Set the ReturnColor property from the RadioButton controls
                foreach (UIElement child in radioStack.Children)
                    if ((child as RadioButton).IsChecked.Value)
                        returnData.ReturnColor = (Color)(child as RadioButton).Tag;

                // Fire the Completed event
                Completed(this, returnData);
            }
            base.OnNavigatedFrom(e);
        }

        private void OnReturnButtonClick(object sender, RoutedEventArgs e) {
            this.Frame.GoBack();
        }
    }
}
