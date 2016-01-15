using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace DataBindingTester.Views {
    /// <acknowledgement>
    /// http://blog.jerrynixon.com/2013/07/solved-two-way-binding-inside-user.html
    /// </acknowledgement>
    public sealed partial class ReUseableUserControl1 : UserControl {
        public ReUseableUserControl1() {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        // FirstNameUC property
        public string FirstNameUC {
            get { return (string)GetValue(FirstNameUCProperty); }
            set { SetValueDp(FirstNameUCProperty, value); }
        }
        public static readonly DependencyProperty FirstNameUCProperty =
            DependencyProperty.Register("FirstNameUC", typeof(string), typeof(ReUseableUserControl1), new PropertyMetadata(0));

        // NamesUC property
        public ObservableCollection<string> NamesUC {
            get { return (ObservableCollection<string>)GetValue(NamesUCProperty); }
            set { SetValueDp(NamesUCProperty, value); }
        }
        public static readonly DependencyProperty NamesUCProperty =
            DependencyProperty.Register("NamesUC", typeof(ObservableCollection<string>), typeof(ReUseableUserControl1), new PropertyMetadata(0));

        // NamesCvsUC property
        public CollectionViewSource NamesCvsUC {
            get { return (CollectionViewSource)GetValue(NamesCvsUCProperty); }
            set { SetValueDp(NamesCvsUCProperty, value); }
        }
        public static readonly DependencyProperty NamesCvsUCProperty =
            DependencyProperty.Register("NamesCvsUC", typeof(CollectionViewSource), typeof(ReUseableUserControl1), new PropertyMetadata(0));

        // NamesCollectionViewUC property
        public ICollectionView NamesCollectionViewUC {
            get { return (ICollectionView)GetValue(NamesCollectionViewUCProperty); }
            set { SetValueDp(NamesCollectionViewUCProperty, value); }
        }
        public static readonly DependencyProperty NamesCollectionViewUCProperty =
            DependencyProperty.Register("NamesCollectionViewUC", typeof(ICollectionView), typeof(ReUseableUserControl1), new PropertyMetadata(0));

        // reuse
        public event PropertyChangedEventHandler PropertyChanged;
        void SetValueDp(DependencyProperty property, object value, [System.Runtime.CompilerServices.CallerMemberName] String p = null) {
            SetValue(property, value);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }
    }
}
