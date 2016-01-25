using Prism.Commands;
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
    public sealed partial class DataOperationsUC : UserControl {
        public DataOperationsUC() {
            this.InitializeComponent();
        }

        #region CreateCommand DP

        public DelegateCommand CreateCommand {
            get { return (DelegateCommand)GetValue(CreateCommandProperty); }
            set { SetValue(CreateCommandProperty, value); }
        }

        public static readonly DependencyProperty CreateCommandProperty =
            DependencyProperty.Register("CreateCommand", typeof(DelegateCommand),
                typeof(DataOperationsUC), new PropertyMetadata(""));

        #endregion

        #region ReadCommand DP

        public DelegateCommand ReadCommand {
            get { return (DelegateCommand)GetValue(ReadCommandProperty); }
            set { SetValue(ReadCommandProperty, value); }
        }

        public static readonly DependencyProperty ReadCommandProperty =
            DependencyProperty.Register("ReadCommand", typeof(DelegateCommand),
                typeof(DataOperationsUC), new PropertyMetadata(""));

        #endregion

        #region WriteCommand DP

        public DelegateCommand WriteCommand {
            get { return (DelegateCommand)GetValue(WriteCommandProperty); }
            set { SetValue(WriteCommandProperty, value); }
        }

        public static readonly DependencyProperty WriteCommandProperty =
            DependencyProperty.Register("WriteCommand", typeof(DelegateCommand),
                typeof(DataOperationsUC), new PropertyMetadata(""));

        #endregion

        #region DeleteCommand DP

        public DelegateCommand DeleteCommand {
            get { return (DelegateCommand)GetValue(DeleteCommandProperty); }
            set { SetValue(DeleteCommandProperty, value); }
        }

        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register("DeleteCommand", typeof(DelegateCommand),
                typeof(DataOperationsUC), new PropertyMetadata(""));

        #endregion

        #region Results DP

        public string Results {
            get { return (string)GetValue(ResultsProperty); }
            set { SetValue(ResultsProperty, value); }
        }

        public static readonly DependencyProperty ResultsProperty =
            DependencyProperty.Register("Results", typeof(string),
                typeof(DataOperationsUC), new PropertyMetadata(""));

        #endregion
    }
}
