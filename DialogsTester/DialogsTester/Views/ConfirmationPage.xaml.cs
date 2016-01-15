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
    public sealed partial class ConfirmationPage : Page {
        public ConfirmationPage() {
            this.InitializeComponent();
        }

        private async void OnDelete(object sender, RoutedEventArgs e) {
            var dialog = new MessageDialog(string.Format("Delete Earth?"));
            dialog.Title = "Are you sure?";
            dialog.Commands.Add(new UICommand { Label = "Delete", Id = 1 });
            dialog.Commands.Add(new UICommand { Label = "Cancel", Id = 0 });
            var res = await dialog.ShowAsync();
            if ((int)res.Id == 1) {
                textBlock.Text = "Earth deleted";
            }
            if ((int)res.Id == 0) {
                textBlock.Text = "Earth saved";
            }
        }
    }
}
