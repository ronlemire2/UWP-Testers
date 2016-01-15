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
    public sealed partial class ManualBrushAnimationPage : Page {
        public ManualBrushAnimationPage() {
            this.InitializeComponent();
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, object e) {
            RenderingEventArgs renderingArgs = e as RenderingEventArgs;
            double t = (0.25 * renderingArgs.RenderingTime.TotalSeconds) % 1;
            t = t < 0.5 ? 2 * t : 2 - 2 * t;

            //Background
            byte gray = (byte)(255 * t);
            //Color clr = Color.FromArgb(255, gray, gray, gray);
            //contentGrid.Background = new SolidColorBrush(clr);
            gridBrush.Color = Color.FromArgb(255, gray, gray, gray);

            // Foreground
            gray = (byte)(255 - gray);
            //clr = Color.FromArgb(255, gray, gray, gray);
            //txtblk.Foreground = new SolidColorBrush(clr);
            txtblkBrush.Color = Color.FromArgb(255, gray, gray, gray);

        }
    }
}
