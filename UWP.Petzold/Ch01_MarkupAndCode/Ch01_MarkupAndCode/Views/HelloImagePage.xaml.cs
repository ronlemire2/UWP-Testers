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

namespace Ch01_MarkupAndCode.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HelloImagePage : Page {
        public HelloImagePage() {
            this.InitializeComponent();
            rbNone.IsChecked = true;
        }

        private void rbStretch_Checked(object sender, RoutedEventArgs e) {
            imgHello.Stretch = StringToStretch(((RadioButton)sender).Content.ToString());
        }

        private Stretch StringToStretch(string value) {
            Stretch result = Stretch.None;
            switch (value.ToLower()) {
                case "none":
                    result = Stretch.None;
                    break;
                case "fill":
                    result = Stretch.Fill;
                    break;
                case "uniform":
                    result = Stretch.Uniform;
                    break;
                case "uniformtofill":
                    result = Stretch.UniformToFill;
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
