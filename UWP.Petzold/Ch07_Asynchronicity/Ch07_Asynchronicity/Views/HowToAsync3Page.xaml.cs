using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Ch07_Asynchronicity.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HowToAsync3Page : Page {
        public HowToAsync3Page() {
            this.InitializeComponent();
        }

        private async void OnButtonClick(object sender, RoutedEventArgs e) {
            MessageDialog msgdlg = new MessageDialog("Choose a color", "How to Async #1");
            msgdlg.Commands.Add(new UICommand("Red", null, Colors.Red));
            msgdlg.Commands.Add(new UICommand("Green", null, Colors.Green));
            msgdlg.Commands.Add(new UICommand("Blue", null, Colors.Blue));

            //// Show the MessageDialog
            //IUICommand command = await msgdlg.ShowAsync();

            //// Get the Color value
            //Color clr = (Color)command.Id;

            //// Set the background brush
            //contentGrid.Background = new SolidColorBrush(clr);

            contentGrid.Background = new SolidColorBrush((Color)(await msgdlg.ShowAsync()).Id);
        }
    }
}
