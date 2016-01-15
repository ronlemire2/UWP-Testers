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
    public sealed partial class HowToCancelAsyncPage : Page {
        IAsyncOperation<IUICommand> asyncOp;

        public HowToCancelAsyncPage() {
            this.InitializeComponent();
        }

        async void OnButtonClick(object sender, RoutedEventArgs e) {
            MessageDialog msgdlg = new MessageDialog("Choose a color", "How to Async #1");
            msgdlg.Commands.Add(new UICommand("Red", null, Colors.Red));
            msgdlg.Commands.Add(new UICommand("Green", null, Colors.Green));
            msgdlg.Commands.Add(new UICommand("Blue", null, Colors.Blue));

            // Start a five-second timer
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += OnTimerTick;
            timer.Start();

            // Show the MessageDialog
            asyncOp = msgdlg.ShowAsync();
            IUICommand command = null;

            try {
                command = await asyncOp;
            }
            catch (Exception) {
                // The exception in this case will be TaskCanceledException
            }

            // Stop the timer
            timer.Stop();

            // If the operation was canceled, exit the method
            if (command == null) {
                return;
            }

            // Get the Color value and set the background brush
            Color clr = (Color)command.Id;
            contentGrid.Background = new SolidColorBrush(clr);
        }

        void OnTimerTick(object sender, object args) {
            // Cancel the asynchronous operation
            asyncOp.Cancel();
        }
    }
}
