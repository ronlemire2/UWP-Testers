using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Ch05_ControlInteraction.Controls {
    public class LineJoinRadioButton : RadioButton {
        public PenLineJoin LineJoinTag { set; get; }
    }
}
