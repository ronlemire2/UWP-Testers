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

namespace Ch01_MarkupAndCode.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HelloCodePage : Page {
        TextBlock tbHelloCode;

        public HelloCodePage() {
            this.InitializeComponent();

            // Set text properties on the Page object and have them inherited by TextBlock
            this.FontFamily = new FontFamily("Times New Roman");
            this.FontSize = 96;
            this.FontStyle = Windows.UI.Text.FontStyle.Italic;
            this.Foreground = new SolidColorBrush(Colors.Blue);

            tbHelloCode = new TextBlock {
                Text = "Hello Windows 10!",
                //FontFamily = new FontFamily("Times New Roman"),
                //FontSize = 96,
                //FontStyle = Windows.UI.Text.FontStyle.Italic,
                //Foreground = new SolidColorBrush(Colors.Blue),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            // Set FontStyle using Dependency Property
            //tbHelloCode.SetValue(TextBlock.FontStyleProperty, Windows.UI.Text.FontStyle.Italic);

            contentGrid.Children.Add(tbHelloCode);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

            // Optional place to add TextBlock to Grid. Could also do contruction here.
            //contentGrid.Children.Add(tbHelloCode);
        }
    }
}
