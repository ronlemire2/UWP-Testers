using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class SuspendResumeLogPage : Page {
        StorageFile logfile;

        public SuspendResumeLogPage() {
            this.InitializeComponent();

            Loaded += OnLoaded;
            Application.Current.Suspending += OnAppSuspending;
            Application.Current.Resuming += OnAppResuming;
        }

        async void OnLoaded(object sender, RoutedEventArgs args) {
            // Create or obtain the log file
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            logfile = await localFolder.CreateFileAsync("logfile.txt", CreationCollisionOption.OpenIfExists);

            // Load the file and display it
            txtbox.Text = await FileIO.ReadTextAsync(logfile);
            //txtbox.Text = await PathIO.ReadTextAsync("ms-appdata:///local/logfile.txt");
            await FileIO.WriteTextAsync(logfile, txtbox.Text);
        }

        async void OnAppSuspending(object sender, SuspendingEventArgs args) {
            SuspendingDeferral deferral = args.SuspendingOperation.GetDeferral();

            // Log the suspension
            txtbox.Text += String.Format("Suspending at {0}\r\n", DateTime.Now.ToString());
            await FileIO.WriteTextAsync(logfile, txtbox.Text);
            //await PathIO.WriteTextAsync("ms-appdata:///local/logfile.txt", txtbox.Text);

            deferral.Complete();
        }

        async void OnAppResuming(object sender, object args) {
            // Log the resumption
            txtbox.Text += String.Format("Resuming at {0}\r\n", DateTime.Now.ToString());
            await FileIO.WriteTextAsync(logfile, txtbox.Text);
            //await PathIO.WriteTextAsync("ms-appdata:///local/logfile.txt", txtbox.Text);
        }

    }
}
