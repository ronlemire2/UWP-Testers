using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Ch01_MarkupAndCode.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HelloImageCodePage : Page {
        public HelloImageCodePage() {
            this.InitializeComponent();

            Uri uri = new Uri("http://www.charlespetzold.com/pw6/PetzoldJersey.jpg");
            BitmapImage bitmap = new BitmapImage(uri);
            Image image = new Image();
            image.Source = bitmap;

            // use grid Name to access contentGrid
            //contentGrid.Children.Add(image);

            // use Content of Page (i.e. contentGrid) to get Grid
            Grid grid = this.Content as Grid;
            grid.Children.Add(image);

        }
    }
}
