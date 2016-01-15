using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Ch11_Templates.ViewModels {
    public class NamedColor {
        static NamedColor() {
            List<NamedColor> colorList = new List<NamedColor>();
            IEnumerable<PropertyInfo> properties = typeof(Colors).GetTypeInfo().DeclaredProperties;

            foreach (PropertyInfo property in properties) {
                NamedColor namedColor = new NamedColor {
                    Name = property.Name,
                    Color = (Color)property.GetValue(null)
                };

                colorList.Add(namedColor);
            }

            All = colorList;
        }

        public static IEnumerable<NamedColor> All { private set; get; }

        public string Name { private set; get; }

        public Color Color { private set; get; }
    }
}
