﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Ch07_Asynchronicity.Converters {
    public class BooleanToVisibilityConverter : IValueConverter {
        public bool IsReversed { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language) {
            var val = System.Convert.ToBoolean(value);
            if (this.IsReversed) {
                val = !val;
            }

            if (val) {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
