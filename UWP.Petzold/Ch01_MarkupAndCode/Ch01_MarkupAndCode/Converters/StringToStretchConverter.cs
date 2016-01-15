using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Ch01_MarkupAndCode.Converters {
    public class StringToStretchConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            Stretch result = Stretch.None;
            switch (((string)value).ToLower()) {
                case "none":
                    result = Stretch.None;
                    break;
                case "fill":
                    result = Stretch.Fill;
                    break;
                case "uniform":
                    result = Stretch.Uniform;
                    break;
                case "uniformtofill":
                    result = Stretch.Uniform;
                    break;
                default:
                    break;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
