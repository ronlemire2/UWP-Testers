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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace AsyncTester.Views {
    public sealed partial class LocalDataUC : UserControl {
        public LocalDataUC() {
            this.InitializeComponent();
            this.Loaded += new RoutedEventHandler(LocalDataUC_Loaded);
        }

        // Id property
        public string Id {
            get { return (string)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(string), typeof(LocalDataUC), new PropertyMetadata(""));

        // Name property
        public new string Name {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public static new readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(LocalDataUC), new PropertyMetadata(""));

        // ImagePath property
        public string ImagePath {
            get { return (string)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }
        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register("ImagePath", typeof(string), typeof(LocalDataUC), new PropertyMetadata(""));

        // Mass property
        public string Mass {
            get { return (string)GetValue(MassProperty); }
            set { SetValue(MassProperty, value); }
        }
        public static readonly DependencyProperty MassProperty =
            DependencyProperty.Register("Mass", typeof(string), typeof(LocalDataUC), new PropertyMetadata(""));

        // Diameter property
        public string Diameter {
            get { return (string)GetValue(DiameterProperty); }
            set { SetValue(DiameterProperty, value); }
        }
        public static readonly DependencyProperty DiameterProperty =
            DependencyProperty.Register("Diameter", typeof(string), typeof(LocalDataUC), new PropertyMetadata(""));

        // Gravity property
        public string Gravity {
            get { return (string)GetValue(GravityProperty); }
            set { SetValue(GravityProperty, value); }
        }
        public static readonly DependencyProperty GravityProperty =
            DependencyProperty.Register("Gravity", typeof(string), typeof(LocalDataUC), new PropertyMetadata(""));

        // LengthOfDay property
        public string LengthOfDay {
            get { return (string)GetValue(LengthOfDayProperty); }
            set { SetValue(LengthOfDayProperty, value); }
        }
        public static readonly DependencyProperty LengthOfDayProperty =
            DependencyProperty.Register("LengthOfDay", typeof(string), typeof(LocalDataUC), new PropertyMetadata(""));

        // DistanceFromSun property
        public string DistanceFromSun {
            get { return (string)GetValue(DistanceFromSunProperty); }
            set { SetValue(DistanceFromSunProperty, value); }
        }
        public static readonly DependencyProperty DistanceFromSunProperty =
            DependencyProperty.Register("DistanceFromSun", typeof(string), typeof(LocalDataUC), new PropertyMetadata(""));

        // OrbitalPeriod property
        public string OrbitalPeriod {
            get { return (string)GetValue(OrbitalPeriodProperty); }
            set { SetValue(OrbitalPeriodProperty, value); }
        }
        public static readonly DependencyProperty OrbitalPeriodProperty =
            DependencyProperty.Register("OrbitalPeriod", typeof(string), typeof(LocalDataUC), new PropertyMetadata(""));

        // MeanTemperature property
        public string MeanTemperature {
            get { return (string)GetValue(MeanTemperatureProperty); }
            set { SetValue(MeanTemperatureProperty, value); }
        }
        public static readonly DependencyProperty MeanTemperatureProperty =
            DependencyProperty.Register("MeanTemperature", typeof(string), typeof(LocalDataUC), new PropertyMetadata(""));

        // NumberOfMoons property
        public string NumberOfMoons {
            get { return (string)GetValue(NumberOfMoonsProperty); }
            set { SetValue(NumberOfMoonsProperty, value); }
        }
        public static readonly DependencyProperty NumberOfMoonsProperty =
            DependencyProperty.Register("NumberOfMoons", typeof(string), typeof(LocalDataUC), new PropertyMetadata(""));

        // ErrorsText property
        public string ErrorsText {
            get { return (string)GetValue(ErrorsTextProperty); }
            set { SetValue(ErrorsTextProperty, value); }
        }
        public static readonly DependencyProperty ErrorsTextProperty =
            DependencyProperty.Register("ErrorsText", typeof(string), typeof(LocalDataUC), new PropertyMetadata(""));

        // http://stackoverflow.com/questions/10051226/how-to-set-isreadonly-isenabled-on-entire-container-like-panel-or-groupbox-usi
        // TextBoxesAreEnabled property
        public bool TextBoxesAreEnabled {
            get { return (bool)GetValue(TextBoxesAreEnabledProperty); }
            set { SetValue(TextBoxesAreEnabledProperty, value); }
        }
        public static readonly DependencyProperty TextBoxesAreEnabledProperty =
            DependencyProperty.Register("TextBoxesAreEnabled", typeof(bool), typeof(LocalDataUC), new PropertyMetadata(false));

        void LocalDataUC_Loaded(object sender, RoutedEventArgs e) {
            this.SetIsEnabledOfChildren();
        }

        private void SetIsEnabledOfChildren() {
            foreach (object o in leftStackPanel.Children) {
                if (o is TextBox) {
                    ((TextBox)o).IsEnabled = TextBoxesAreEnabled;
                }
            }
            foreach (object o in rightStackPanel.Children) {
                if (o is TextBox) {
                    ((TextBox)o).IsEnabled = TextBoxesAreEnabled;
                }
            }
        }

    }
}
