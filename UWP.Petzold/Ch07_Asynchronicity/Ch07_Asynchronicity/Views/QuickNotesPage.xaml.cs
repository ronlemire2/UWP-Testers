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
    public sealed partial class QuickNotesPage : Page {
        public QuickNotesPage() {
            this.InitializeComponent();
            Loaded += OnLoaded;
            Application.Current.Suspending += OnAppSuspending;
        }

        async void OnLoaded(object sender, RoutedEventArgs args) {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await localFolder.CreateFileAsync("QuickNotes.txt", CreationCollisionOption.OpenIfExists);
            txtblk.Text = await FileIO.ReadTextAsync(storageFile);
            txtblk.SelectionStart = txtblk.Text.Length;
            txtblk.Focus(FocusState.Programmatic);
        }

        async void OnAppSuspending(object sender, SuspendingEventArgs args) {
            SuspendingDeferral deferral = args.SuspendingOperation.GetDeferral();
            await PathIO.WriteTextAsync("ms-appdata:///local/QuickNotes.txt", txtblk.Text);
            deferral.Complete();
        }
    }
}
