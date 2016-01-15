using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Ch12_PagesAndNavigation.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StudentPage : Page {
        public StudentPage() {
            this.InitializeComponent();
            SizeChanged += OnPageSizeChanged;
        }

        void OnPageSizeChanged(object sender, SizeChangedEventArgs args) {
            VisualStateManager.GoToState(this, ApplicationView.Value.ToString(), true);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            this.DataContext = e.Parameter;
            base.OnNavigatedTo(e);
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e) {
            this.Frame.GoBack();
        }
    }
}
