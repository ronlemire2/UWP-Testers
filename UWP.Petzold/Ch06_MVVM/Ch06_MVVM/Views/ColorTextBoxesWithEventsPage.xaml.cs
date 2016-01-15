using Ch06_MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Ch06_MVVM.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ColorTextBoxesWithEventsPage : Page {
        RgbViewModel rgbViewModel;
        Brush textBoxTextBrush;
        Brush textBoxErrorBursh = new SolidColorBrush(Colors.Red);
        //bool blockViewModelUpdates;

        public ColorTextBoxesWithEventsPage() {
            this.InitializeComponent();

            // Get TextBox brush
            textBoxTextBrush = this.Resources["TextBoxForegroundThemeBrush"] as SolidColorBrush;

            // Create RgbViewModel and save as field
            rgbViewModel = new RgbViewModel();
            rgbViewModel.PropertyChanged += OnRgbViewModelPropertyChanged;
            this.DataContext = rgbViewModel;

            // Initialize to highlight color
            rgbViewModel.Color = new UISettings().UIElementColor(UIElementType.Highlight);
        }

        public void OnRgbViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
            //if (blockViewModelUpdates)
            //    return;

            switch (e.PropertyName) {
                case "Red":
                    redTextBox.Text = rgbViewModel.Red.ToString("F0");
                    break;
                case "Green":
                    greenTextBox.Text = rgbViewModel.Green.ToString("F0");
                    break;
                case "Blue":
                    blueTextBox.Text = rgbViewModel.Blue.ToString("F0");
                    break;
                default:
                    break;
            }
        }

        private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e) {
            byte value;

            //blockViewModelUpdates = true;

            if (sender == redTextBox && Validate(redTextBox, out value)) {
                rgbViewModel.Red = value;
            }

            if (sender == greenTextBox && Validate(greenTextBox, out value)) {
                rgbViewModel.Green = value;
            }

            if (sender == blueTextBox && Validate(blueTextBox, out value)) {
                rgbViewModel.Blue = value;
            }

            //blockViewModelUpdates = false;
        }

        bool Validate(TextBox txtbox, out byte value) {
            bool valid = byte.TryParse(txtbox.Text, out value);
            txtbox.Foreground = valid ? textBoxTextBrush : textBoxErrorBursh;
            return valid;
        }
    }
}
