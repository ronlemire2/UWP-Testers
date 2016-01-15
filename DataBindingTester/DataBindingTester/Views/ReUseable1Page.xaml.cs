using DataBindingTester.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DataBindingTester.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReUseable1Page : Page {
        public ReUseable1Page() {
            this.InitializeComponent();

            ReUseable1PageViewModel vm = new ReUseable1PageViewModel();
            vm.Names = new ObservableCollection<string> { "ron", "clara" };
            vm.NamesCvs = new CollectionViewSource();
            vm.NamesCvs.Source = vm.Names;
            vm.NamesCollectionView = vm.NamesCvs.View;
            this.DataContext = vm;
        }
    }
}
