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
    public sealed partial class SliderEventsPage : Page {
        public SliderEventsPage() {
            this.InitializeComponent();
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e) {
            Slider slider = sender as Slider;
            Panel parentPanel = slider.Parent as Panel;
            int childIndex = parentPanel.Children.IndexOf(slider);
            TextBlock txtblk = parentPanel.Children[childIndex + 1] as TextBlock;
            //txtblk.Text = e.NewValue.ToString();
            txtblk.Text = slider.Value.ToString();
        }
    }
}
