using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
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
    public sealed partial class SimpleContextMenuPage : Page {
        public SimpleContextMenuPage() {
            this.InitializeComponent();
        }

        async void OnTextBlockRightTapped(object sender, RightTappedRoutedEventArgs e) {
            PopupMenu popupMenu = new PopupMenu();
            popupMenu.Commands.Add(new UICommand("Larger Font", OnFontSizeChanged, 1.2));
            popupMenu.Commands.Add(new UICommand("Smaller Font", OnFontSizeChanged, 1 / 1.2));
            popupMenu.Commands.Add(new UICommandSeparator());
            popupMenu.Commands.Add(new UICommand("Red", OnColorChanged, Colors.Red));
            popupMenu.Commands.Add(new UICommand("Green", OnColorChanged, Colors.Green));
            popupMenu.Commands.Add(new UICommand("Blue", OnColorChanged, Colors.Blue));

            await popupMenu.ShowAsync(e.GetPosition(this));
        }

        void OnFontSizeChanged(IUICommand command) {
            txtblk.FontSize *= (double)command.Id;
        }

        void OnColorChanged(IUICommand command) {
            txtblk.Foreground = new SolidColorBrush((Color)command.Id);
        }
    }
}
