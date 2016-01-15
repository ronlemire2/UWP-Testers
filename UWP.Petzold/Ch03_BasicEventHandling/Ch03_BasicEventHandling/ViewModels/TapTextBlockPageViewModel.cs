using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch03_BasicEventHandling.ViewModels {
    public class TapTextBlockPageViewModel : BindableBase {

        private string _rgb0;
        public string rgb0 {
            get { return _rgb0; }
            set { SetProperty(ref _rgb0, value); }
        }

        private string _rgb1;
        public string rgb1 {
            get { return _rgb1; }
            set { SetProperty(ref _rgb1, value); }
        }

        private string _rgb2;
        public string rgb2 {
            get { return _rgb2; }
            set { SetProperty(ref _rgb2, value); }
        }
    }
}
