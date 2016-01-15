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

namespace Ch05_ControlInteraction.Views {
    public sealed partial class GradientButtonUC : Button {
        public GradientButtonUC() {
            this.InitializeComponent();
        }

        public Color Color1 {
            set { SetValue(Color1Property, value); }
            get { return (Color)GetValue(Color1Property); }
        }

        public Color Color2 {
            set { SetValue(Color2Property, value); }
            get { return (Color)GetValue(Color2Property); }
        }

        public static DependencyProperty Color1Property { private set; get; }
        public static DependencyProperty Color2Property { private set; get; }

        static GradientButtonUC() {
            Color1Property =
                DependencyProperty.Register("Color1",
                    typeof(Color),
                    typeof(GradientButtonUC),
                    new PropertyMetadata(Colors.White));

            Color2Property =
                DependencyProperty.Register("Color2",
                    typeof(Color),
                    typeof(GradientButtonUC),
                    new PropertyMetadata(Colors.Black));
        }
    }
}
