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

namespace Ch05_ControlInteraction.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SimpleColorScrollPage : Page {
        public SimpleColorScrollPage() {
            this.InitializeComponent();
        }

        private void OnSliderValueChanged(object sender, RangeBaseValueChangedEventArgs e) {
            //Color clr = Color.FromArgb(
            //    (byte)255,
            //    (byte)redSlider.Value,
            //    (byte)greenSlider.Value,
            //    (byte)blueSlider.Value);
            //rectangle.Fill = new SolidColorBrush(clr);

            //redValue.Text = ((byte)redSlider.Value).ToString("X2");
            //greenValue.Text = ((byte)greenSlider.Value).ToString("X2");
            //blueValue.Text = ((byte)blueSlider.Value).ToString("X2");

            byte r = (byte)redSlider.Value;
            byte g = (byte)greenSlider.Value;
            byte b = (byte)blueSlider.Value;

            redValue.Text = r.ToString("X2");
            greenValue.Text = g.ToString("X2");
            blueValue.Text = b.ToString("X2");

            brushResult.Color = Color.FromArgb(255, r, g, b);
        }
    }
}
