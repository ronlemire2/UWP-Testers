using Prism.Windows.Mvvm;
using Prism.Windows.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace AsyncTester.ViewModels {
    public class PlanetViewModel : ValidatableBindableBase {
        private string id;
        public string Id {
            get { return id; }
            set { SetProperty<string>(ref id, value); }
        }

        private string name;
        public string Name {
            get { return name; }
            set { SetProperty<string>(ref name, value); }
        }

        private string imagePath;
        public string ImagePath {
            get { return imagePath; }
            set { SetProperty<string>(ref imagePath, value); }
        }

        private string mass;
        public string Mass {
            get { return mass; }
            set { SetProperty<string>(ref mass, value); }
        }

        private string diameter;
        public string Diameter {
            get { return diameter; }
            set { SetProperty<string>(ref diameter, value); }
        }

        private string gravity;
        public string Gravity {
            get { return gravity; }
            set { SetProperty<string>(ref gravity, value); }
        }

        private string lengthOfDay;
        public string LengthOfDay {
            get { return lengthOfDay; }
            set { SetProperty<string>(ref lengthOfDay, value); }
        }

        private string distanceFromSun;
        public string DistanceFromSun {
            get { return distanceFromSun; }
            set { SetProperty<string>(ref distanceFromSun, value); }
        }

        private string orbitalPeriod;
        public string OrbitalPeriod {
            get { return orbitalPeriod; }
            set { SetProperty<string>(ref orbitalPeriod, value); }
        }

        private string meanTemperature;
        public string MeanTemperature {
            get { return meanTemperature; }
            set { SetProperty<string>(ref meanTemperature, value); }
        }

        private string numberOfMoons;
        public string NumberOfMoons {
            get { return numberOfMoons; }
            set { SetProperty<string>(ref numberOfMoons, value); }
        }

        public ImageSource PlanetImage {
            get {
                // ImagePath contains a path of the form "Earth.png".
                return new BitmapImage(new Uri(new Uri("ms-appdata:///local/"), this.ImagePath));
            }
        }

        private bool textBoxesAreEnabled;
        public bool TextBoxesAreEnabled {
            get { return textBoxesAreEnabled; }
            set { SetProperty<bool>(ref textBoxesAreEnabled, value); }
        }
    }
}
