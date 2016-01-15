using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Ch01_MarkupAndCode {
    /// <summary>
    /// Data to represent an item in the nav menu.
    /// </summary>
    public class NavMenuItem {
        public string Label { get; set; }
        public Symbol Symbol { get; set; }
        public char SymbolAsChar {
            get {
                //return (char)Symbol;  // Symbol values in AppShell.xaml.cs
                return (char)57354;  // Filled Star (black)
                //return (char)9312;    // Circled '1' set FontFamily to 'Segoe UI Symbol' in AppShell.xaml
            }
        }

        public Type DestPage { get; set; }
        public object Arguments { get; set; }
        public string PageToken { get; set; }
    }
}
