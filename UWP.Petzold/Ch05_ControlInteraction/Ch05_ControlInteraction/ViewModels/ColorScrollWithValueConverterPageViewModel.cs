using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Ch05_ControlInteraction.ViewModels {
    public class ColorScrollWithValueConverterPageViewModel : BindableBase {
        public ColorScrollWithValueConverterPageViewModel() {
            ColorScrollValue = Colors.Black;
        }

        private double redSliderValue;
        public double RedSliderValue {
            get {
                return redSliderValue;
            }
            set {
                SetProperty(ref redSliderValue, value);
                SetColorScrollValue();
            }
        }

        private double greenSliderValue;
        public double GreenSliderValue {
            get {
                return greenSliderValue;
            }
            set {
                SetProperty(ref greenSliderValue, value);
                SetColorScrollValue();
            }
        }

        private double blueSliderValue;
        public double BlueSliderValue {
            get {
                return blueSliderValue;
            }
            set {
                SetProperty(ref blueSliderValue, value);
                SetColorScrollValue();
            }
        }

        private Color colorScrollValue;
        public Color ColorScrollValue {
            get {
                return colorScrollValue;
            }
            set {
                SetProperty(ref colorScrollValue, value);
            }
        }

        private void SetColorScrollValue() {
            Color scrollColor = new Color();
            scrollColor.A = 255;
            scrollColor.R = (byte)RedSliderValue;
            scrollColor.G = (byte)GreenSliderValue;
            scrollColor.B = (byte)BlueSliderValue;
            ColorScrollValue = scrollColor;
        }

    }
}
