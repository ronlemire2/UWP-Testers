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

/// <Acknowledgement>
/// http://blog.scottlogic.com/2012/02/06/a-simple-pattern-for-creating-re-useable-usercontrols-in-wpf-silverlight.html  
/// </Acknowledgement>
namespace DataBindingTester.Views {
    public sealed partial class ReUseableUserControl2 : UserControl {
        #region Label DP

        public string Label {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string),
                typeof(ReUseableUserControl2), new PropertyMetadata(""));

        #endregion


        #region Value DP

        public object Value {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object),
                typeof(ReUseableUserControl2), new PropertyMetadata(null));

        #endregion

        public ReUseableUserControl2() {
            this.InitializeComponent();
        }
    }
}
