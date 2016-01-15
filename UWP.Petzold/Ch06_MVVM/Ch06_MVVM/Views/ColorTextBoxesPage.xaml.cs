using Ch06_MVVM.ViewModels;
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

namespace Ch06_MVVM.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ColorTextBoxesPage : Page {
        public ColorTextBoxesPage() {
            this.InitializeComponent();
        }

        private void OnGridLoaded(object sender, RoutedEventArgs e) {
            ((sender as Grid).DataContext as RgbViewModel).Color =
                new UISettings().UIElementColor(UIElementType.Highlight);
        }
    }
}
