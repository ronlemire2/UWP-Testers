using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch03_BasicEventHandling.ViewModels {
    public class WhatSizePageViewModel : BindableBase {
        private string _widthText;
        public string WidthText {
            get {
                return _widthText;
            }
            set {
                SetProperty(ref _widthText, value);
            }
        }

        private string _heightText;
        public string HeightText {
            get {
                return _heightText;
            }
            set {
                SetProperty(ref _heightText, value);
            }
        }
    }
}
