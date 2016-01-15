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

namespace DialogsTester.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TermsOfUsePage : Page {
        public TermsOfUsePage() {
            this.InitializeComponent();
        }

        async void OnShowTermsOfUse(object sender, RoutedEventArgs e) {
            ContentDialogResult result = await termsOfUseContentDialog.ShowAsync();
            if (result == ContentDialogResult.Primary) {
                // Terms of use were accepted.
            }
            else {
                // User pressed Cancel or the back arrow.
                // Terms of use were not accepted.
            }
        }

        private void TermsOfUseContentDialog_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args) {
            // Ensure that the check box is unchecked each time the dialog opens.
            ConfirmAgeCheckBox.IsChecked = false;
        }

        private void ConfirmAgeCheckBox_Checked(object sender, RoutedEventArgs e) {
            termsOfUseContentDialog.IsPrimaryButtonEnabled = true;
        }

        private void ConfirmAgeCheckBox_Unchecked(object sender, RoutedEventArgs e) {
            termsOfUseContentDialog.IsPrimaryButtonEnabled = false;
        }
    }
}
