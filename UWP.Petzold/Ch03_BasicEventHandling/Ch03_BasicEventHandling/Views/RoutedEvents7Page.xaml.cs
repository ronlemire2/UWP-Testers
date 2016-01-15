using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Ch03_BasicEventHandling.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RoutedEvents7Page : Page {
        Random rand = new Random();
        byte[] rgb = new byte[3];

        public RoutedEvents7Page() {
            this.InitializeComponent();
        }

        protected override void OnTapped(TappedRoutedEventArgs args) {
            rand.NextBytes(rgb);
            Color clr = Color.FromArgb(255, rgb[0], rgb[1], rgb[2]);
            SolidColorBrush brush = new SolidColorBrush(clr);

            if (args.OriginalSource is TextBlock) {
                (args.OriginalSource as TextBlock).Foreground = brush;
            }
            else if (args.OriginalSource is Grid) {
                (args.OriginalSource as Grid).Background = brush;
            }
            base.OnTapped(args);
        }
    }
}
