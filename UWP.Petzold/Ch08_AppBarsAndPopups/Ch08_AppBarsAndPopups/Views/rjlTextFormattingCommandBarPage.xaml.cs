using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Ch08_AppBarsAndPopups.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class rjlTextFormattingCommandBarPage : Page {
        public rjlTextFormattingCommandBarPage() {
            this.InitializeComponent();
        }

        private void OnBoldAppBarCheckBoxChecked(object sender, RoutedEventArgs e) {
            CheckBox chkbox = sender as CheckBox;
            txtblk.FontWeight = (bool)chkbox.IsChecked ? FontWeights.Bold : FontWeights.Normal;
        }

        private void OnItalicAppBarCheckBoxChecked(object sender, RoutedEventArgs e) {
            CheckBox chkbox = sender as CheckBox;
            txtblk.FontStyle = (bool)chkbox.IsChecked ? FontStyle.Italic : FontStyle.Normal;
        }

        private void OnUnderlineAppBarCheckBoxChecked(object sender, RoutedEventArgs e) {
            CheckBox chkbox = sender as CheckBox;
            Inline inline = txtblk.Inlines[0];

            if ((bool)chkbox.IsChecked && !(inline is Underline)) {
                Underline underline = new Underline();
                txtblk.Inlines[0] = underline;
                underline.Inlines.Add(inline);
            }
            else if (!(bool)chkbox.IsChecked && inline is Underline) {
                Underline underline = inline as Underline;
                Run run = underline.Inlines[0] as Run;
                underline.Inlines.Clear();
                txtblk.Inlines[0] = run;
            }
        }

        private void OnFontColorAppBarRadioButtonChecked(object sender, RoutedEventArgs e) {
            txtblk.Foreground = (sender as RadioButton).Foreground;
        }
    }
}
