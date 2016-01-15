using Ch05_ControlInteraction.Controls;
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
    public sealed partial class LineCapsAndJoinsWithCustomClassPage : Page {
        public LineCapsAndJoinsWithCustomClassPage() {
            this.InitializeComponent();

            Loaded += (sender, e) => {
                foreach (UIElement child in startLineCapPanel.Children)
                    (child as LineCapRadioButton).IsChecked =
                        (child as LineCapRadioButton).LineCapTag == polyline.StrokeStartLineCap;

                foreach (UIElement child in endLineCapPanel.Children)
                    (child as LineCapRadioButton).IsChecked =
                        (child as LineCapRadioButton).LineCapTag == polyline.StrokeEndLineCap;

                foreach (UIElement child in lineJoinPanel.Children)
                    (child as LineJoinRadioButton).IsChecked =
                        (child as LineJoinRadioButton).LineJoinTag == polyline.StrokeLineJoin;
            };
        }

        private void OnStartLineCapRadioButtonChecked(object sender, RoutedEventArgs e) {
            polyline.StrokeStartLineCap = (sender as LineCapRadioButton).LineCapTag;
        }

        private void OnLineJoinRadioButtonChecked(object sender, RoutedEventArgs e) {
            polyline.StrokeLineJoin = (sender as LineJoinRadioButton).LineJoinTag;
        }

        private void OnEndLineCapRadioButtonChecked(object sender, RoutedEventArgs e) {
            polyline.StrokeEndLineCap = (sender as LineCapRadioButton).LineCapTag;
        }
    }
}
