﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace AdaptiveTester.Converters {
    class ListConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return String.Join("\r\n", (IEnumerable<string>)value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
