﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace Ch04_Panels.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StackPanelWithScrollingPage : Page {
        public StackPanelWithScrollingPage() {
            this.InitializeComponent();

            IEnumerable<PropertyInfo> properties = typeof(Colors).GetTypeInfo().DeclaredProperties;

            foreach (PropertyInfo property in properties) {
                Color clr = (Color)property.GetValue(null);
                TextBlock txtblk = new TextBlock();
                txtblk.Text = String.Format("{0} \x2014 {1:X2}-{2:X2}-{3:X2}-{4:X2}",
                    property.Name, clr.A, clr.R, clr.G, clr.B);
                stackPanel.Children.Add(txtblk);
            }
        }
    }
}
