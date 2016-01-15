using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Ch08_AppBarsAndPopups.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LookAtAppBarButtonStylesPage : Page {
        class Item {
            public string Key;
            public char Symbol;
            public string Text;
        }

        List<Item> appbarStyles = new List<Item>();
        FontFamily segoeSymbolFont = new FontFamily("Segoe UI Symbol");

        public LookAtAppBarButtonStylesPage() {
            this.InitializeComponent();
        }

        private void OnRadioButtonChecked(object sender, RoutedEventArgs e) {
            if (sender == symbolSortRadio) {
                // Sort by symbol
                appbarStyles.Sort((item1, Item2) => {
                    return item1.Symbol.CompareTo(Item2.Symbol);
                });
            }
            else {
                // Sort by text
                appbarStyles.Sort((item1, Item2) => {
                    return item1.Text.CompareTo(Item2.Text);
                });
            }

            // Close app bar and display the items
            BottomAppBar.IsOpen = false;
            DisplayList();
        }

        void OnLoaded(object sender, RoutedEventArgs args) {
            // Basically gets StandardStyles.xaml
            ResourceDictionary dictionary = Application.Current.Resources.MergedDictionaries[0];
            Style baseStyle = dictionary["AppBarButtonStyle"] as Style;

            // Find all styles based on AppBarButtonStyle
            try {
                foreach (object key in dictionary.Keys) {
                    Style style = dictionary[key] as Style;

                    if (style != null && style.BasedOn == baseStyle) {
                        Item item = new Item {
                            Key = key as string
                        };

                        foreach (Setter setter in style.Setters) {
                            if (setter.Property.Equals(AutomationProperties.NameProperty))
                                item.Text = setter.Value as string;

                            if (setter.Property.Equals(ButtonBase.ContentProperty))
                                item.Symbol = (setter.Value as string)[0];
                        }

                        appbarStyles.Add(item);
                    }
                }
            }
            catch (Exception x) {
                
            }

            // Display items by checking RadioButton
            textSortRadio.IsChecked = true;
        }

        void DisplayList() {
            // Clear the StackPanel
            stackPanel.Children.Clear();

            // Loop through the styles
            foreach (Item item in appbarStyles) {
                // A StackPanel for each item
                StackPanel itemPanel = new StackPanel {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 6, 0, 6)
                };

                // The symbol itself
                TextBlock textBlock = new TextBlock {
                    Text = item.Symbol.ToString(),
                    FontFamily = segoeSymbolFont,
                    Margin = new Thickness(24, 0, 24, 0)
                };
                itemPanel.Children.Add(textBlock);

                // The Unicode identifier
                textBlock = new TextBlock {
                    Text = "0x" + ((int)item.Symbol).ToString("X4"),
                    Width = 96
                };
                itemPanel.Children.Add(textBlock);

                // The text for the button
                textBlock = new TextBlock {
                    Text = "\"" + item.Text + "\"",
                    Width = 240,
                };
                itemPanel.Children.Add(textBlock);

                // The key name
                textBlock = new TextBlock {
                    Text = item.Key
                };
                itemPanel.Children.Add(textBlock);

                stackPanel.Children.Add(itemPanel);
            }
        }
    }
}
