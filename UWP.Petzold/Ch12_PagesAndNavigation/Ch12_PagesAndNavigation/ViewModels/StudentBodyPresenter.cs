using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Ch12_PagesAndNavigation.ViewModels {
    public class StudentBodyPresenter : BindableBase {
        StudentBody studentBody;
        Random rand = new Random();
        Window currentWindow = Window.Current;

        public StudentBodyPresenter() {
            // Download XML file
            HttpClient httpClient = new HttpClient();
            Task<string> task =
                httpClient.GetStringAsync("http://www.charlespetzold.com/Students/students.xml");
            task.ContinueWith(GetStringCompleted);
        }

        async void GetStringCompleted(Task<string> task) {
            if (task.Exception == null && !task.IsCanceled) {
                string xml = task.Result;

                // Deserialize XML
                StringReader reader = new StringReader(xml);
                XmlSerializer serializer = new XmlSerializer(typeof(StudentBody));

                await currentWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    this.StudentBody = serializer.Deserialize(reader) as StudentBody;

                    // Set a timer for random changes
                    DispatcherTimer timer = new DispatcherTimer {
                        Interval = TimeSpan.FromMilliseconds(100)
                    };
                    timer.Tick += OnTimerTick;
                    timer.Start();
                });
            }
        }

        public StudentBody StudentBody {
            set { SetProperty<StudentBody>(ref studentBody, value); }
            get { return studentBody; }
        }

        // Mimic changing grade point averages
        void OnTimerTick(object sender, object args) {
            int index = rand.Next(studentBody.Students.Count);
            Student student = this.StudentBody.Students[index];
            double factor = 1 + (rand.NextDouble() - 0.5) / 5;
            student.GradePointAverage =
                Math.Max(0.0, Math.Min(5.0, (int)(100 * factor * student.GradePointAverage) / 100.0));
        }
    }
}
