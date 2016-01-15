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
    public sealed partial class SimpleKeyPadPage : Page {
        string inputString = string.Empty;
        char[] specialChars = { '*', '#' };

        public SimpleKeyPadPage() {
            this.InitializeComponent();
        }

        private void OnDeleteButtonClick(object sender, RoutedEventArgs e) {
            inputString = inputString.Substring(0, inputString.Length - 1);
            FormatText();
        }

        private void OnCharButtonClick(object sender, RoutedEventArgs e) {
            Button btn = sender as Button;
            inputString += btn.Content as String;
            FormatText();
        }

        void FormatText() {
            bool hasNonNumbers = inputString.IndexOfAny(specialChars) != -1;

            if (hasNonNumbers || inputString.Length < 4 || inputString.Length > 10)
                resultText.Text = inputString;
            else if (inputString.Length < 8)
                resultText.Text = String.Format("{0}-{1}", 
                    inputString.Substring(0, 3), inputString.Substring(3));
            else
                resultText.Text = String.Format("({0}) {1}-{2}",
                    inputString.Substring(0, 3), inputString.Substring(3, 3), inputString.Substring(6));

            deleteButton.IsEnabled = inputString.Length > 0;
        }
    }
}
