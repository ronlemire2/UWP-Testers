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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Ch03_BasicEventHandling.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RainbowEightPage : Page {
        double txtblkBaseSize;

        public RainbowEightPage() {
            this.InitializeComponent();
            Loaded += RainbowEightPage_Loaded;
        }

        private void RainbowEightPage_Loaded(object sender, RoutedEventArgs e) {
            txtblkBaseSize = txtblk.ActualHeight;
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, object e) {
            // Set FontSize as large as it can be
            txtblk.FontSize = this.ActualHeight / txtblkBaseSize;

            // Calculate t from 0 to 1 repetitively
            RenderingEventArgs renderingArgs = e as RenderingEventArgs;
            double t = (0.25 * renderingArgs.RenderingTime.TotalSeconds) % 1;

            // Loop through GradientStop objects
            for (int index = 0; index < gradientBrush.GradientStops.Count; index++) {
                gradientBrush.GradientStops[index].Offset = index / 7.0 - t;
            }
        }
    }
}
