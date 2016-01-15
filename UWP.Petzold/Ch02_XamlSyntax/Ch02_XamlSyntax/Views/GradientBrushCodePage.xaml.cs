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

namespace Ch02_XamlSyntax.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GradientBrushCodePage : Page {
        public GradientBrushCodePage() {
            this.InitializeComponent();

            // Create the foreground brush for the TextBlock
            LinearGradientBrush foregroundBrush = new LinearGradientBrush();
            foregroundBrush.StartPoint = new Point(0, 0);
            foregroundBrush.EndPoint = new Point(1, 0);

            GradientStop gradientStop = new GradientStop();
            gradientStop.Offset = 0;
            gradientStop.Color = Colors.Blue;
            foregroundBrush.GradientStops.Add(gradientStop);

            gradientStop = new GradientStop();
            gradientStop.Offset = 1;
            gradientStop.Color = Colors.Red;
            foregroundBrush.GradientStops.Add(gradientStop);

            txtblk.Foreground = foregroundBrush;

            // Create the background brush for the Grid
            LinearGradientBrush backgroundBrush = new LinearGradientBrush {StartPoint = new Point(0, 0), EndPoint = new Point(1, 0)};
            backgroundBrush.GradientStops.Add(new GradientStop { Offset = 0, Color = Colors.Red });
            backgroundBrush.GradientStops.Add(new GradientStop { Offset = 1, Color = Colors.Blue });
            contentGrid.Background = backgroundBrush;
        }
    }
}
