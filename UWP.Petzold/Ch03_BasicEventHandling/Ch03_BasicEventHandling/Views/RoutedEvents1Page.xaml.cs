using Ch03_BasicEventHandling.ViewModels;
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
    public sealed partial class RoutedEvents1Page : Page {
        private static readonly Random rand = new Random();
        byte[] rgb = new byte[3];
        TapTextBlockPageViewModel vm;

        public RoutedEvents1Page() {
            this.InitializeComponent();
            vm = new TapTextBlockPageViewModel();
            this.DataContext = vm;
        }

        private void OnGridTapped(object sender, TappedRoutedEventArgs args) {
            //if (args.OriginalSource is TextBlock) {
            //    TextBlock txtblk = args.OriginalSource as TextBlock;
            TextBlock txtblk = args.OriginalSource as TextBlock;
            if (txtblk != null) {
                rand.NextBytes(rgb);
                Color clr = Color.FromArgb(255, rgb[0], rgb[1], rgb[2]);
                txtblk.Foreground = new SolidColorBrush(clr);
                vm.rgb0 = string.Format("Red: {0}", rgb[0].ToString());
                vm.rgb1 = string.Format("Green: {0}", rgb[1].ToString());
                vm.rgb2 = string.Format("Blue: {0}", rgb[2].ToString());
            }
            else {
                vm.rgb0 = "RoutedEvents1Page: Grid tapped";
                vm.rgb1 = string.Empty;
                vm.rgb2 = string.Empty;
            }
        }
    }
}
