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
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Ch04_Panels.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TapAndShowPointPage : Page {
        public TapAndShowPointPage() {
            this.InitializeComponent();
        }

        protected override void OnTapped(TappedRoutedEventArgs e) {
            Point pt = e.GetPosition(this);

            // Create dot
            Ellipse ellipse = new Ellipse {
                Width = 3,
                Height = 3,
                Fill = this.Foreground
            };

            Canvas.SetLeft(ellipse, pt.X);
            Canvas.SetTop(ellipse, pt.Y);
            canvas.Children.Add(ellipse);

            // Create text
            TextBlock txtblk = new TextBlock {
                Text = String.Format("{0}", pt),
                FontSize = 24
            };

            Canvas.SetLeft(txtblk, pt.X);
            Canvas.SetTop(txtblk, pt.Y);
            canvas.Children.Add(txtblk);

            e.Handled = true;

            base.OnTapped(e);
        }
    }
}
