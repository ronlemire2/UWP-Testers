using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
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
    public sealed partial class ScalableInternationalHelloWorldPage : Page {
        public ScalableInternationalHelloWorldPage() {
            this.InitializeComponent();
            //DisplayProperties.OrientationChanged += OnDisplayPropertiesOrientatioChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e) {
            pageWidth.Text = e.NewSize.Width.ToString();
        }

        //private void OnDisplayPropertiesOrientatioChanged(object sender) {
        //    SetFont();
        //}

        //private void SetFont() {
        //    bool isLandscape =
        //        // Windows 8.1
        //        //DisplayProperties.CurrentOrientation == DisplayOrientations.Landscape ||
        //        //DisplayProperties.CurrentOrientation == DisplayOrientations.LandscapeFlipped;

        //        //DisplayInformation.GetForCurrentView().CurrentOrientation == DisplayOrientations.Landscape ||
        //        //DisplayInformation.GetForCurrentView().CurrentOrientation == DisplayOrientations.Landscape;
        //}
    }
}
