using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace Ch11_Templates.ViewModels {
    public class Clock : BindableBase {
        bool isEnabled;
        int hour, minute, second;
        int hourAngle, minuteAngle, secondAngle;

        public bool IsEnabled {
            set {
                if (SetProperty<bool>(ref isEnabled, value, "IsEnabled")) {
                    if (isEnabled)
                        CompositionTarget.Rendering += OnCompositionTargetRendering;
                    else
                        CompositionTarget.Rendering -= OnCompositionTargetRendering;
                }
            }
            get {
                return isEnabled;
            }
        }

        public int Hour {
            set { SetProperty<int>(ref hour, value); }
            get { return hour; }
        }

        public int Minute {
            set { SetProperty<int>(ref minute, value); }
            get { return minute; }
        }

        public int Second {
            set { SetProperty<int>(ref second, value); }
            get { return second; }
        }

        public int HourAngle {
            set { SetProperty<int>(ref hourAngle, value); }
            get { return hourAngle; }
        }

        public int MinuteAngle {
            set { SetProperty<int>(ref minuteAngle, value); }
            get { return minuteAngle; }
        }
        public int SecondAngle {
            set { SetProperty<int>(ref secondAngle, value); }
            get { return secondAngle; }
        }

        void OnCompositionTargetRendering(object sender, object args) {
            DateTime dateTime = DateTime.Now;
            this.Hour = dateTime.Hour;
            this.Minute = dateTime.Minute;
            this.Second = dateTime.Second;

            this.HourAngle = 30 * dateTime.Hour + dateTime.Minute / 2;
            this.MinuteAngle = 6 * dateTime.Minute + dateTime.Second / 10;
            this.SecondAngle = 6 * dateTime.Second + dateTime.Millisecond / 166;
        }

    }
}
