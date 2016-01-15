using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Ch11_Templates.Converters {
    public class BooleanToVisibilityConverter : IValueConverter {
        // From Petzold 6 Ch11 p462
        public object Convert(object value, Type targetType, object parameter, string language) {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return (Visibility)value == Visibility.Visible;
        }


        //public bool IsReversed { get; set; }

        //public object Convert(object value, Type targetType, object parameter, string language) {
        //    var val = System.Convert.ToBoolean(value);
        //    if (this.IsReversed) {
        //        val = !val;
        //    }

        //    if (val) {
        //        return Visibility.Visible;
        //    }

        //    return Visibility.Collapsed;
        //}

        //public object ConvertBack(object value, Type targetType, object parameter, string language) {
        //    throw new NotImplementedException();
        //}
    }
}
