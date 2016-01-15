﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Ch02_XamlSyntax.Controls;
using Ch02_XamlSyntax.Views;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Ch02_XamlSyntax {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppShell : Page {
        // Declare the top level nav items
        private List<NavMenuItem> navlist = new List<NavMenuItem>(
            new[]
            {
                new NavMenuItem()
                {
                    Symbol = Symbol.SolidStar,
                    Label = "GradientBrushCode",
                    DestPage = typeof(GradientBrushCodePage),
                    PageToken = "GradientBrushCode"
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.SolidStar,
                    Label = "GradientBrushMarkup",
                    DestPage = typeof(GradientBrushMarkupPage),
                    PageToken = "GradientBrushMarkup"
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.SolidStar,
                    Label = "ImplicitStyle",
                    DestPage = typeof(ImplicitStylePage),
                    PageToken = "ImplicitStyle"
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.SolidStar,
                    Label = "SharedBrush",
                    DestPage = typeof(SharedBrushPage),
                    PageToken = "SharedBrush"
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.SolidStar,
                    Label = "SharedBrushWithBinding",
                    DestPage = typeof(SharedBrushWithBindingPage),
                    PageToken = "SharedBrushWithBinding"
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.SolidStar,
                    Label = "SharedBrushWithStyle",
                    DestPage = typeof(SharedBrushWithStylePage),
                    PageToken = "SharedBrushWithStyle"
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.SolidStar,
                    Label = "TextFormatting",
                    DestPage = typeof(TextFormattingPage),
                    PageToken = "TextFormatting"
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.SolidStar,
                    Label = "TextStretch",
                    DestPage = typeof(TextStretchPage),
                    PageToken = "TextStretch"
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.SolidStar,
                    Label = "VectorGraphicsStretch",
                    DestPage = typeof(VectorGraphicsStretchPage),
                    PageToken = "VectorGraphicsStretch"
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.SolidStar,
                    Label = "rjlUserControlStretch",
                    DestPage = typeof(rjlUserControlStretchPage),
                    PageToken = "rjlUserControlStretch"
                },
            });

        public static AppShell Current = null;


        /// <summary>
        /// Initializes a new instance of the AppShell, sets the static 'Current' reference,
        /// adds callbacks for Back requests and changes in the SplitView's DisplayMode, and
        /// provide the nav menu list with the data to display.
        /// </summary>
        public AppShell() {

            this.InitializeComponent();

            this.Loaded += (sender, args) => {
                Current = this;

                //this.TogglePaneButton.Focus(FocusState.Programmatic);
            };

            this.splitView.RegisterPropertyChangedCallback(
                SplitView.DisplayModeProperty,
                (s, a) => {
                    // Ensure that we update the reported size of the TogglePaneButton when the SplitView's
                    // DisplayMode changes.
                    this.CheckTogglePaneButtonSizeChanged();
                });

            //SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;

            // If on a phone device that has hardware buttons then we hide the app's back button.
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")) {
                this.backButton.Visibility = Visibility.Collapsed;
            }

            NavMenuList.ItemsSource = navlist;
        }

        public Frame AppFrame { get { return this.frame; } }

        /// <summary>
        /// Default keyboard focus movement for any unhandled keyboarding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppShell_KeyDown(object sender, KeyRoutedEventArgs e) {
            FocusNavigationDirection direction = FocusNavigationDirection.None;
            switch (e.Key) {
                case Windows.System.VirtualKey.Left:
                case Windows.System.VirtualKey.GamepadDPadLeft:
                case Windows.System.VirtualKey.GamepadLeftThumbstickLeft:
                case Windows.System.VirtualKey.NavigationLeft:
                    direction = FocusNavigationDirection.Left;
                    break;
                case Windows.System.VirtualKey.Right:
                case Windows.System.VirtualKey.GamepadDPadRight:
                case Windows.System.VirtualKey.GamepadLeftThumbstickRight:
                case Windows.System.VirtualKey.NavigationRight:
                    direction = FocusNavigationDirection.Right;
                    break;

                case Windows.System.VirtualKey.Up:
                case Windows.System.VirtualKey.GamepadDPadUp:
                case Windows.System.VirtualKey.GamepadLeftThumbstickUp:
                case Windows.System.VirtualKey.NavigationUp:
                    direction = FocusNavigationDirection.Up;
                    break;

                case Windows.System.VirtualKey.Down:
                case Windows.System.VirtualKey.GamepadDPadDown:
                case Windows.System.VirtualKey.GamepadLeftThumbstickDown:
                case Windows.System.VirtualKey.NavigationDown:
                    direction = FocusNavigationDirection.Down;
                    break;
            }

            if (direction != FocusNavigationDirection.None) {
                var control = FocusManager.FindNextFocusableElement(direction) as Control;
                if (control != null) {
                    control.Focus(FocusState.Programmatic);
                    e.Handled = true;
                }
            }
        }

        #region BackRequested Handlers

        //private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e) {
        //    bool handled = e.Handled;
        //    this.BackRequested(ref handled);
        //    e.Handled = handled;
        //}

        private void BackButton_Click(object sender, RoutedEventArgs e) {
            // Get a hold of the current frame so that we can inspect the app back stack.
            if (this.AppFrame == null)
                return;

            // Check to see if this is the top-most page on the app back stack.
            if (this.AppFrame.CanGoBack) {
                // If not, set the event to handled and go back to the previous page in the app.
                this.AppFrame.GoBack();
            }
        }

        #endregion

        #region Navigation

        /// <summary>
        /// Navigate to the Page for the selected <paramref name="listViewItem"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="listViewItem"></param>
        private void NavMenuList_ItemInvoked(object sender, ListViewItem listViewItem) {
            var item = (NavMenuItem)((NavMenuListView)sender).ItemFromContainer(listViewItem);

            if (item != null) {
                if (item.DestPage != null &&
                    item.DestPage != this.AppFrame.CurrentSourcePageType) {
                    this.AppFrame.Navigate(item.DestPage, item.Arguments);
                }
            }
        }

        /// <summary>
        /// Ensures the nav menu reflects reality when navigation is triggered outside of
        /// the nav menu buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNavigatingToPage(object sender, NavigatingCancelEventArgs e) {
            if (e.NavigationMode == NavigationMode.Back) {
                var item = (from p in this.navlist where p.DestPage == e.SourcePageType select p).SingleOrDefault();
                if (item == null && this.AppFrame.BackStackDepth > 0) {
                    // In cases where a page drills into sub-pages then we'll highlight the most recent
                    // navigation menu item that appears in the BackStack
                    foreach (var entry in this.AppFrame.BackStack.Reverse()) {
                        item = (from p in this.navlist where p.DestPage == entry.SourcePageType select p).SingleOrDefault();
                        if (item != null)
                            break;
                    }
                }

                var container = (ListViewItem)NavMenuList.ContainerFromItem(item);

                // While updating the selection state of the item prevent it from taking keyboard focus.  If a
                // user is invoking the back button via the keyboard causing the selected nav menu item to change
                // then focus will remain on the back button.
                if (container != null) container.IsTabStop = false;
                NavMenuList.SetSelectedItem(container);
                if (container != null) container.IsTabStop = true;
            }
        }

        private void OnNavigatedToPage(object sender, NavigationEventArgs e) {
            // After a successful navigation set keyboard focus to the loaded page
            if (e.Content is Page && e.Content != null) {
                var control = (Page)e.Content;
                control.Loaded += Page_Loaded;
            }

            // Update the Back button depending on whether we can go Back.
            //SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
            //    AppFrame.CanGoBack ?
            //    AppViewBackButtonVisibility.Visible :
            //    AppViewBackButtonVisibility.Collapsed;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            ((Page)sender).Focus(FocusState.Programmatic);
            ((Page)sender).Loaded -= Page_Loaded;
            this.CheckTogglePaneButtonSizeChanged();
            titleTextBlock.Text = ((Page)sender).Name;
        }

        #endregion

        public Rect TogglePaneButtonRect {
            get;
            private set;
        }

        /// <summary>
        /// An event to notify listeners when the hamburger button may occlude other content in the app.
        /// The custom "PageHeader" user control is using this.
        /// </summary>
        public event TypedEventHandler<AppShell, Rect> TogglePaneButtonRectChanged;

        /// <summary>
        /// Callback when the SplitView's Pane is toggled open or close.  When the Pane is not visible
        /// then the floating hamburger may be occluding other content in the app unless it is aware.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TogglePaneButton_Checked(object sender, RoutedEventArgs e) {
            this.CheckTogglePaneButtonSizeChanged();
        }

        /// <summary>
        /// Check for the conditions where the navigation pane does not occupy the space under the floating
        /// hamburger button and trigger the event.
        /// </summary>
        private void CheckTogglePaneButtonSizeChanged() {
            if (this.splitView.DisplayMode == SplitViewDisplayMode.Inline ||
                this.splitView.DisplayMode == SplitViewDisplayMode.Overlay) {
                //var transform = this.TogglePaneButton.TransformToVisual(this);
                //var rect = transform.TransformBounds(new Rect(0, 0, this.TogglePaneButton.ActualWidth, this.TogglePaneButton.ActualHeight));
                //this.TogglePaneButtonRect = rect;
            }
            else {
                this.TogglePaneButtonRect = new Rect();
            }

            var handler = this.TogglePaneButtonRectChanged;
            if (handler != null) {
                // handler(this, this.TogglePaneButtonRect);
                handler.DynamicInvoke(this, this.TogglePaneButtonRect);
            }
        }

        /// <summary>
        /// Enable accessibility on each nav menu item by setting the AutomationProperties.Name on each container
        /// using the associated Label of each item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void NavMenuItemContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args) {
            if (!args.InRecycleQueue && args.Item != null && args.Item is NavMenuItem) {
                args.ItemContainer.SetValue(AutomationProperties.NameProperty, ((NavMenuItem)args.Item).Label);
            }
            else {
                args.ItemContainer.ClearValue(AutomationProperties.NameProperty);
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e) {
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

    }
}
