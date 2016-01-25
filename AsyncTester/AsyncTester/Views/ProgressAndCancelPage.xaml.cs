using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AsyncTester.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProgressAndCancelPage : Page {
        const int TASK_DELAY_MS = 1000;
        const double MAX_INTERVALS = 5.0;
        double intervalCount = 0;
        CancellationTokenSource cts;

        public ProgressAndCancelPage() {
            this.InitializeComponent();
        }

        private async void OnStartButtonClick(object sender, RoutedEventArgs e) {
            startButton.IsEnabled = false;
            status.Text = "";
            progressBar.Value = 0;

            try {
                cancelButton.IsEnabled = true;
                cts = new CancellationTokenSource();
                await LongTaskAsync(cts.Token, new Progress<double>(ProgressCallback));
                status.Text = "Completed";
            }
            catch (OperationCanceledException) {
                status.Text = "Cancelled";
            }
            catch (Exception exc) {
                status.Text = "Error: " + exc.Message;
            }
            finally {
                startButton.IsEnabled = true;
                cancelButton.IsEnabled = false;
                progressBar.Value = 0;
                intervalCount = 0;
            }
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e) {
            cts.Cancel();
        }


        void ProgressCallback(double progress) {
            progressBar.Value = progress;
        }

        async Task LongTaskAsync(CancellationToken cancellationToken, IProgress<double> progress) {
            while (intervalCount < MAX_INTERVALS) {
                await DelayAsync();
                cancellationToken.ThrowIfCancellationRequested();
                intervalCount++;
                progress.Report(100 * (intervalCount / MAX_INTERVALS));
            }
            return;
        }

        async Task DelayAsync() {
            await Task.Delay(TASK_DELAY_MS);
        }
    }
}
