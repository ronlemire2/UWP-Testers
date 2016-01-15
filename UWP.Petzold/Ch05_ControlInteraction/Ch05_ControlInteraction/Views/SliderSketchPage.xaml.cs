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

namespace Ch05_ControlInteraction.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SliderSketchPage : Page {
        public SliderSketchPage() {
            this.InitializeComponent();
        }

        private void OnSliderValueChanged(object sender, RangeBaseValueChangedEventArgs e) {
            polyline.Points.Add(new Point(xSlider.Value, ySlider.Value));
        }

        private void OnBorderSizeChanged(object sender, SizeChangedEventArgs e) {
            Border border = sender as Border;
            xSlider.Maximum = e.NewSize.Width - border.Padding.Left - border.Padding.Right - polyline.StrokeThickness;
            ySlider.MaxHeight = e.NewSize.Height - border.Padding.Top - border.Padding.Bottom - polyline.StrokeThickness;
        }
    }
}
