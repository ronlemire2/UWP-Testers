﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DialogsTester.Views {
    public enum SignInResult {
        SignInOK,
        SignInFail,
        SignInCancel,
        Nothing
    }

    /// <remarks>
    /// Attribution:
    /// https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.contentdialog.aspx#events
    /// https://github.com/Microsoft/Windows-universal-samples/blob/master/Samples/XamlUIBasics/cs/AppUIBasics/ControlPages/ContentDialogExample.xaml
    /// </remarks>
    public sealed partial class SignInContentDialog : ContentDialog {
        public SignInResult Result { get; private set; }

        public SignInContentDialog() {
            this.InitializeComponent();
            this.Opened += SignInContentDialog_Opened;
            this.Closing += SignInContentDialog_Closing;
        }

        async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) {
            // Ensure the user name and password fields aren't empty. If a required field
            // is empty, set args.Cancel = true to keep the dialog open.
            if (string.IsNullOrEmpty(userNameTextBox.Text)) {
                args.Cancel = true;
                errorTextBlock.Text = "User name is required.";
            }
            else if (string.IsNullOrEmpty(passwordTextBox.Password)) {
                args.Cancel = true;
                errorTextBlock.Text = "Password is required.";
            }

            // If you're performing async operations in the button click handler,
            // get a deferral before you await the operation. Then, complete the
            // deferral when the async operation is complete.

            ContentDialogButtonClickDeferral deferral = args.GetDeferral();
            if (await SomeAsyncSignInOperation(passwordTextBox.Password)) {
                this.Result = SignInResult.SignInOK;
            }
            else {
                this.Result = SignInResult.SignInFail;
            }
            deferral.Complete();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) {
            // User clicked Cancel.
            this.Result = SignInResult.SignInCancel;
        }

        void SignInContentDialog_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args) {
            this.Result = SignInResult.Nothing;

            // If the user name is saved, get it and populate the user name field.
            Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            if (roamingSettings.Values.ContainsKey("userName")) {
                userNameTextBox.Text = roamingSettings.Values["userName"].ToString();
                saveUserNameCheckBox.IsChecked = true;
            }
        }

        void SignInContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args) {
            // If sign in was successful, save or clear the user name based on the user choice.
            if (this.Result == SignInResult.SignInOK) {
                if (saveUserNameCheckBox.IsChecked == true) {
                    SaveUserName();
                }
                else {
                    ClearUserName();
                }
            }

            // If the user entered a name and checked or cleared the 'save user name' checkbox, then clicked the back arrow,
            // confirm if it was their intention to save or clear the user name without signing in. 
            if (this.Result == SignInResult.Nothing && !string.IsNullOrEmpty(userNameTextBox.Text)) {
                if (saveUserNameCheckBox.IsChecked == false) {
                    args.Cancel = true;
                    FlyoutBase.SetAttachedFlyout(this, (FlyoutBase)this.Resources["DiscardNameFlyout"]);
                    FlyoutBase.ShowAttachedFlyout(this);
                }
                else if (saveUserNameCheckBox.IsChecked == true && !string.IsNullOrEmpty(userNameTextBox.Text)) {
                    args.Cancel = true;
                    FlyoutBase.SetAttachedFlyout(this, (FlyoutBase)this.Resources["SaveNameFlyout"]);
                    FlyoutBase.ShowAttachedFlyout(this);
                }
            }
        }

        private void SaveUserName() {
            Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            roamingSettings.Values["userName"] = userNameTextBox.Text;
        }

        private void ClearUserName() {
            Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            roamingSettings.Values["userName"] = null;
            userNameTextBox.Text = string.Empty;
        }

        private void Flyout_Closed(object sender, object e) {
            this.Hide();
        }

        private void DiscardButton_Click(object sender, RoutedEventArgs e) {
            ClearUserName();
            FlyoutBase.GetAttachedFlyout(this).Hide();
        }

        // Handle the button clicks from the flyouts.
        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            SaveUserName();
            FlyoutBase.GetAttachedFlyout(this).Hide();
        }

        async Task<bool> SomeAsyncSignInOperation(string password) {
            await Task.Delay(1000);

            if (password.Any(char.IsPunctuation)) {
                return false;
            }
            else {
                return true;
            }
        }
    }
}
