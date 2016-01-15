using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingTester.ViewModels {
    public class ReUseable2PageViewModel : BindableBase {
        private int shoesize;
        public int Shoesize {
            get { return shoesize; }
            set {
                SetProperty<int>(ref shoesize, value);
            }
        }

        private double height;
        public double Height {
            get { return height; }
            set {
                SetProperty<double>(ref height, value);
            }
        }
    }
}
