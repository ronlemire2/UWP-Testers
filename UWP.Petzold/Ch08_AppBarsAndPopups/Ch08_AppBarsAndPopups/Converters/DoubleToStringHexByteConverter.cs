﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Ch08_AppBarsAndPopups.Converters {
    public class DoubleToStringHexByteConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return ((int)(double)value).ToString("X2") + "__";
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return value;
        }
    }
}
