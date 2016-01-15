using Ch12_PagesAndNavigation.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class VisitedPageSavePage : SaveStatePage {
        public VisitedPageSavePage() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs args) {
            // Enable buttons
            forwardButton.IsEnabled = this.Frame.CanGoForward;
            backButton.IsEnabled = this.Frame.CanGoBack;

            // Construct a dictionary key
            int pageKey = this.Frame.BackStackDepth;

            if (args.NavigationMode != NavigationMode.New) {
                // Get the page state dictionary for this page
                pageState = pages[pageKey];

                // Get the page state from the dictionary
                txtbox.Text = pageState["TextBoxText"] as string;
            }

            base.OnNavigatedTo(args);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e) {
            pageState.Clear();

            // Save the page state in the dictionary
            pageState.Add("TextBoxText", txtbox.Text);
            base.OnNavigatedFrom(e);
        }

        private void OnGotoButtonClick(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(SecondSaveStatePage));
        }

        private void OnForwardButtonClick(object sender, RoutedEventArgs e) {
            this.Frame.GoForward();
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e) {
            this.Frame.GoBack();
        }
    }
}
