using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Ch06_MVVM.Converters {
    public class DoubleToStringHexByteConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return ((int)(double)value).ToString("X2");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return (double)int.Parse(value as string, System.Globalization.NumberStyles.HexNumber);
        }
    }
}
