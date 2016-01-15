using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Ch05_ControlInteraction.Controls {
    public class GradientButton : Button {

        GradientStop gradientStop1, gradientStop2;

        public GradientButton() {
            gradientStop1 = new GradientStop { Offset = 0, Color = this.Color1 };
            gradientStop2 = new GradientStop { Offset = 1, Color = this.Color2 };

            LinearGradientBrush brush = new LinearGradientBrush();
            brush.GradientStops.Add(gradientStop1);
            brush.GradientStops.Add(gradientStop2);

            this.Foreground = brush;
        }

        public Color Color1 {
            set { SetValue(Color1Property, value); }
            get { return (Color)GetValue(Color1Property); }
        }

        public Color Color2 {
            set { SetValue(Color2Property, value); }
            get { return (Color)GetValue(Color2Property); }
        }

        #region Dependency Property Creation Option1 - static constructor

        public static DependencyProperty Color1Property { private set; get; }
        public static DependencyProperty Color2Property { private set; get; }

        static GradientButton() {
            Color1Property =
                DependencyProperty.Register("Color1",
                    typeof(Color),
                    typeof(GradientButton),
                    new PropertyMetadata(Colors.White, OnColorChanged));

            Color2Property =
                DependencyProperty.Register("Color2",
                    typeof(Color),
                    typeof(GradientButton),
                    new PropertyMetadata(Colors.Black, OnColorChanged));
        }

        static void OnColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            (obj as GradientButton).OnColorChanged(e);
        }
        void OnColorChanged(DependencyPropertyChangedEventArgs e) {
            if (e.Property == Color1Property)
                gradientStop1.Color = (Color)e.NewValue;

            if (e.Property == Color2Property) {
                gradientStop2.Color = (Color)e.NewValue;
            }

        }

        #endregion

        #region Dependency Property Creation Option 2 - private static fields with public static properties. static constructor not needed.

        //static readonly DependencyProperty color1Property =
        //    DependencyProperty.Register("Color1",
        //        typeof(Color),
        //        typeof(GradientButton),
        //        new PropertyMetadata(Colors.White, OnColorChanged));


        //static readonly DependencyProperty color2Property =
        //    DependencyProperty.Register("Color2",
        //        typeof(Color),
        //        typeof(GradientButton),
        //        new PropertyMetadata(Colors.White, OnColorChanged));

        //public static DependencyProperty Color1Property {
        //    get { return color1Property; }
        //}

        //public static DependencyProperty Color2Property {
        //    get { return color2Property; }
        //}

        #endregion

        #region Dependency Property Creation Option 3 public static fields. Least because since winrt controls have properties not fields.

        //public static readonly DependencyProperty Color1Property =
        //        DependencyProperty.Register("Color1",
        //            typeof(Color),
        //            typeof(GradientButton),
        //            new PropertyMetadata(Colors.White, OnColorChanged));

        //public static readonly DependencyProperty Color2Property =
        //    DependencyProperty.Register("Color2",
        //        typeof(Color),
        //        typeof(GradientButton),
        //        new PropertyMetadata(Colors.White, OnColorChanged));

        #endregion


    }
}
