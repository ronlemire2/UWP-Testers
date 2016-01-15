using DataBindingTester.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace DataBindingTester.Converters {
    public class ObjectToPersonViewModelConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return value as PersonViewModel;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return value as Object;
        }
    }
}
