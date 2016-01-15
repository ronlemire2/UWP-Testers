using Ch11_Templates.Utils;
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

namespace Ch11_Templates.Views {
    /// <remarks>
    /// Acknowledgement: MSDN Library Sample
    /// https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.datatemplate.loadcontent
    /// </remarks>
    public sealed partial class rjlLoadContentIntoDataTemplatePage : Page {
        public rjlLoadContentIntoDataTemplatePage() {
            this.InitializeComponent();
        }

        private void OnListBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
            ListBoxItem lbi = (ListBoxItem)((ListBox)sender).SelectedItem;
            SelectDataTemplate(lbi.Content);
        }

        private void SelectDataTemplate(object value) {
            string numberStr = (string)value;
            if (numberStr != null) {
                int num;
                try {
                    num = Convert.ToInt32(numberStr);
                }
                catch {
                    return;
                }

                // Select one of the DataTemplate objects
                DataTemplate dataTemplate;
                if (num % 2 == 0) {
                    dataTemplate = (DataTemplate)rootStackPanel.Resources["evenNumberDataTemplate"];
                }
                else {
                    dataTemplate = (DataTemplate)rootStackPanel.Resources["oddNumberDataTemplate"];
                }

                // LoadContent creates the UIElement objects in the DataTemplate.
                // LoadContent returns the root of the created visual tree as a Child of the Border.
                selectedItemDisplayBorder.Child = (UIElement)dataTemplate.LoadContent();

                // Find the TextBlock in the Border and set its Text to the ListBoxItem's number.
                TextBlock tb = XamlUtils.FindVisualChild<TextBlock>(selectedItemDisplayBorder);
                tb.Text = numberStr;
            }
        }
    }
}
