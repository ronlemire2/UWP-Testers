using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class SignInPage : Page {
        public SignInPage() {
            this.InitializeComponent();
        }

        async void OnShowSignInDialog(object sender, RoutedEventArgs e) {
            SignInContentDialog signInContentDialog = new SignInContentDialog();
            MessageDialog resultMessageDialog = null;
            await signInContentDialog.ShowAsync();

            if (signInContentDialog.Result == SignInResult.SignInOK) {
                resultMessageDialog = new MessageDialog(Environment.NewLine + "Signed In Successful", "Sign In Result");
            }
            else if (signInContentDialog.Result == SignInResult.SignInFail) {
                resultMessageDialog = new MessageDialog(Environment.NewLine + "Signed In Failed - Punctuation characters are not allowed", "Sign In Result");
            }
            else if (signInContentDialog.Result == SignInResult.SignInCancel) {
                resultMessageDialog = new MessageDialog(Environment.NewLine + "Signed In Canceled", "Sign In Result");
            }
            await resultMessageDialog.ShowAsync();
        }
    }
}
