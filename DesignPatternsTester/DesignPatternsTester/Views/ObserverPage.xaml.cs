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

namespace DesignPatternsTester.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ObserverPage : Page {
        Publisher publisher;
        Subscriber1 subscriber1;
        Subscriber2 subscriber2;

        public ObserverPage() {
            this.InitializeComponent();
            publisher = new Publisher(this);
            subscriber1 = new Subscriber1(this);
            subscriber2 = new Subscriber2(this);

            // Subscribe to price change
            publisher.PriceChanged += subscriber1.NotifyMe;
            publisher.PriceChanged += subscriber2.NotifyMe;
        }

        private void UpdatePriceButton_Click(object sender, RoutedEventArgs e) {
            // Make price change
            publisher.LatestPrice = NewPriceTextBox.Text;
        }

        public ListBox Subscriber1ListBoxReference {
            get {
                return Subscriber1ListBox;
            }
        }

        public TextBlock Subscriber2TextBlockReference {
            get {
                return Subscriber2TextBlock;
            }
        }
    }

    public class Publisher {
        private ObserverPage observerPage;
        public Publisher(ObserverPage observerPage) {
            this.observerPage = observerPage;
        }

        // Define delegate and event
        public delegate void PriceChangedDelegate(object price);
        public event PriceChangedDelegate PriceChanged;

        object latestPrice;
        public object LatestPrice {
            set {
                latestPrice = value;
                PriceChanged(latestPrice); // Raise event
            }
        }
    }

    public class Subscriber1 {
        private ObserverPage observerPage;
        public Subscriber1(ObserverPage observerPage) {
            this.observerPage = observerPage;
        }

        // Event Handler
        public void NotifyMe(object latestPrice) {
            observerPage.Subscriber1ListBoxReference.Items.Add(latestPrice.ToString());
        }
    }

    public class Subscriber2 {
        private ObserverPage observerPage;
        public Subscriber2(ObserverPage observerPage) {
            this.observerPage = observerPage;
        }

        // Event Handler
        public void NotifyMe(object latestPrice) {
            observerPage.Subscriber2TextBlockReference.Text = latestPrice.ToString();
        }
    }

}
