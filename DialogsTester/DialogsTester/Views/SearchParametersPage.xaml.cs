﻿using DialogsTester.ViewModels;
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
    public sealed partial class SearchParametersPage : Page {
        public SearchParametersPage() {
            this.InitializeComponent();
        }

        async void OnShowSearchParametersDialog(object sender, RoutedEventArgs e) {
            // Dialog datacontext contains instance of the viewmodel (done in XAML).
            SearchParametersContentDialog dialog = new SearchParametersContentDialog();

            // Set viewmodel to In values.
            ((SearchParametersViewModel)dialog.DataContext).FirstName = FirstNameIn.Text;
            ((SearchParametersViewModel)dialog.DataContext).LastName = LastNameIn.Text;
            ((SearchParametersViewModel)dialog.DataContext).StateProvinceCode = StateProvinceCodeIn.Text;

            // Show dialog and enter values.
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary) {
                MessageDialog msgDialog = new MessageDialog("OK Pressed");
                await msgDialog.ShowAsync();
            }
            else {
                MessageDialog msgDialog = new MessageDialog("Cancel Pressed");
                await msgDialog.ShowAsync();
            }

            // Viewmodel now has Out values.
            FirstNameOut.Text = ((SearchParametersViewModel)dialog.DataContext).FirstName;
            LastNameOut.Text = ((SearchParametersViewModel)dialog.DataContext).LastName;
            StateProvinceCodeOut.Text = ((SearchParametersViewModel)dialog.DataContext).StateProvinceCode;
        }
    }
}
