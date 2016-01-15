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
    public sealed partial class HowToAsync2Page : Page {
        public HowToAsync2Page() {
            this.InitializeComponent();
        }

        private void OnButtonClick(object sender, RoutedEventArgs e) {
            MessageDialog msgdlg = new MessageDialog("Choose a color", "How to Async #1");
            msgdlg.Commands.Add(new UICommand("Red", null, Colors.Red));
            msgdlg.Commands.Add(new UICommand("Green", null, Colors.Green));
            msgdlg.Commands.Add(new UICommand("Blue", null, Colors.Blue));

            // Show the MessageDialog with a Completed handler
            IAsyncOperation<IUICommand> asyncOp = msgdlg.ShowAsync();
            asyncOp.Completed = (asyncInfo, asyncStatus) => {
                // Get the Color value
                IUICommand command = asyncInfo.GetResults();
                Color clr = (Color)command.Id;

                // Use a Dispatcher to run in the UI thread
                IAsyncAction asyncAction = this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                    // Set the background brush
                    contentGrid.Background = new SolidColorBrush(clr);
                });
            };
            
        }
    }
}
