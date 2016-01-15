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
    public sealed partial class ExpandingTextPage : Page {
        public ExpandingTextPage() {
            this.InitializeComponent();
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, object e) {
            RenderingEventArgs renderArgs = e as RenderingEventArgs;
            double t = (0.25 * renderArgs.RenderingTime.TotalSeconds) % 1;
            double scale = t < 0.5 ? 2 * t : 2 - 2 * t;
            txtblk.FontSize = 1 + scale * 143;
        }
    }
}
