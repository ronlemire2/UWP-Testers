using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Ch06_MVVM.ViewModels {
    /// <summary>
    /// Version used in ColorScrollWithDataContextPage.xaml
    /// </summary>
    public class RgbViewModel : INotifyPropertyChanged {
        double red, green, blue;
        Color color = Color.FromArgb(255, 0, 0, 0);

        public event PropertyChangedEventHandler PropertyChanged;

        public double Red {
            get {
                return red;
            }
            set {
                if (SetProperty<double>(ref red, value, "Red")) {
                    Calculate();
                }
            }
        }

        public double Green {
            get {
                return green;
            }
            set {
                if (SetProperty<double>(ref green, value)) {
                    Calculate();
                }
            }
        }
        public double Blue {
            get {
                return blue;
            }
            set {
                if (SetProperty<double>(ref blue, value)) {
                    Calculate();
                }
            }
        }

        public Color Color {
            get {
                return color;
            }

            set {
                if (SetProperty<Color>(ref color, value)) {
                    this.Red = value.R;
                    this.Green = value.G;
                    this.Blue = value.B;
                }
            }
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null) {
            if (object.Equals(storage, value)) {
                return false;
            }
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        void Calculate() {
            this.Color = Color.FromArgb(255, (byte)this.Red, (byte)this.Green, (byte)this.Blue);
        }
    }



    /// <summary>
    /// Version used in ColorScrollWithViewModelPage.xaml
    /// </summary>
    //public class RgbViewModel : INotifyPropertyChanged {
    //    double red, green, blue;
    //    Color color = Color.FromArgb(255, 0, 0, 0);

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    public double Red {
    //        get {
    //            return red;
    //        }
    //        set {
    //            if (red != value) {
    //                red = value;
    //                OnPropertyChanged("Red");
    //                Calculate();
    //            }
    //        }
    //    }

    //    public double Green {
    //        get {
    //            return green;
    //        }
    //        set {
    //            if (green != value) {
    //                green = value;
    //                OnPropertyChanged("Green");
    //                Calculate();
    //            }
    //        }
    //    }
    //    public double Blue {
    //        get {
    //            return blue;
    //        }
    //        set {
    //            if (blue != value) {
    //                blue = value;
    //                OnPropertyChanged("Blue");
    //                Calculate();
    //            }
    //        }
    //    }

    //    public Color Color {
    //        get {
    //            return color;
    //        }

    //        protected set {
    //            if (color != value) {
    //                color = value;
    //                OnPropertyChanged("Color");
    //            }
    //        }
    //    }

    //    protected void OnPropertyChanged(string propertyName) {
    //        if (PropertyChanged != null) {
    //            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    //        }
    //    }

    //    void Calculate() {
    //        this.Color = Color.FromArgb(255, (byte)this.Red, (byte)this.Green, (byte)this.Blue);
    //    }
    //}
}
