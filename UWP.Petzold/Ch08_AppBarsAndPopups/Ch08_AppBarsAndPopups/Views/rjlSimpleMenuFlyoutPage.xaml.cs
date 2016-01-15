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

namespace Ch08_AppBarsAndPopups.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class rjlSimpleMenuFlyoutPage : Page {
        private bool _IsShiftPressed = false;
        private bool _IsPointerPressed = false;

        public rjlSimpleMenuFlyoutPage() {
            this.InitializeComponent();
        }

        protected override void OnPointerPressed(PointerRoutedEventArgs e) {
            _IsPointerPressed = true;
            base.OnPointerPressed(e);
        }

        protected override void OnRightTapped(RightTappedRoutedEventArgs e) {
            if (_IsPointerPressed) {
                ShowContextMenu(null, e.GetPosition(null));
                e.Handled = true;
            }
            base.OnRightTapped(e);
        }

        private void ShowContextMenu(UIElement target, Point offset) {
            var MyFlyout = this.Resources["rjlMenuFlyout"] as MenuFlyout;

            System.Diagnostics.Debug.WriteLine("MenuFlyout shown '{0}', '{1}'", target, offset);

            MyFlyout.ShowAt(target, offset);
        }

        private void OnColorClick(object sender, RoutedEventArgs e) {
            txtblk.Foreground = ((MenuFlyoutItem)e.OriginalSource).Tag as SolidColorBrush;
        }

        private void OnFontClick(object sender, RoutedEventArgs e) {
            txtblk.FontSize *= (double)((MenuFlyoutItem)e.OriginalSource).Tag;
        }
    }
}
