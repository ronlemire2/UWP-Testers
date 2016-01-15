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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Ch04_Panels.Views {
    public sealed partial class ColorItem : UserControl {
        public ColorItem(string name, Color clr) {
            this.InitializeComponent();

            rectangle.Fill = new SolidColorBrush(clr);
            txtblkName.Text = name;
            txtblkRgb.Text = String.Format("{0:X2}-{1:X2}-{2:X2}-{3:X2}", clr.A, clr.R, clr.G, clr.B);
        }
    }
}
