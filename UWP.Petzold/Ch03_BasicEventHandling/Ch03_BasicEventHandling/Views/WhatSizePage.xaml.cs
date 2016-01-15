using Ch03_BasicEventHandling.ViewModels;
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

namespace Ch03_BasicEventHandling.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WhatSizePage : Page {
        WhatSizePageViewModel vm;

        public WhatSizePage() {
            this.InitializeComponent();
            vm = new WhatSizePageViewModel();
            this.DataContext = vm;
            this.SizeChanged += OnPageSizeChanged;
        }

        private void OnPageSizeChanged(object sender, SizeChangedEventArgs e) {
            vm.WidthText = e.NewSize.Width.ToString();
            vm.HeightText = e.NewSize.Height.ToString();
        }

        //private void OnPageSizeChanged(object sender, SizeChangedEventArgs e) {
        //    widthText.Text = e.NewSize.Width.ToString();
        //    heightText.Text = e.NewSize.Height.ToString();
        //    //widthText.Text = ActualWidth.ToString();
        //    //heightText.Text = ActualHeight.ToString();
        //}
    }
}
