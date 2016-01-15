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
    public sealed partial class LineCapsAndJoinsPage : Page {
        public LineCapsAndJoinsPage() {
            this.InitializeComponent();

            Loaded += (sender, args) => {
                foreach (UIElement child in startLineCapPanel.Children) {
                    (child as RadioButton).IsChecked = (PenLineCap)(child as RadioButton).Tag == polyline.StrokeStartLineCap;
                }
                foreach (UIElement child in endLineCapPanel.Children) {
                    (child as RadioButton).IsChecked = (PenLineCap)(child as RadioButton).Tag == polyline.StrokeEndLineCap;
                }
                foreach (UIElement child in lineJoinPanel.Children) {
                    (child as RadioButton).IsChecked = (PenLineJoin)(child as RadioButton).Tag == polyline.StrokeLineJoin;
                }
            };
        }

        private void OnStartLineCapRadioButtonChecked(object sender, RoutedEventArgs e) {
            polyline.StrokeStartLineCap = (PenLineCap)(sender as RadioButton).Tag;
        }

        private void OnEndLineCapRadioButtonChecked(object sender, RoutedEventArgs e) {
            polyline.StrokeEndLineCap = (PenLineCap)(sender as RadioButton).Tag;
        }

        private void OnLineJoinRadioButtonChecked(object sender, RoutedEventArgs e) {
            polyline.StrokeLineJoin = (PenLineJoin)(sender as RadioButton).Tag;
        }
    }
}
