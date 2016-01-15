using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch12_PagesAndNavigation.ViewModels {
    public class StudentGroups : BindableBase {
        StudentBodyPresenter presenter;
        ObservableCollection<StudentGroup> groups = new ObservableCollection<StudentGroup>();

        public StudentBodyPresenter Source {
            set {
                if (value != null) {
                    presenter = value;
                    presenter.PropertyChanged += OnHighSchoolPropertyChanged;
                }
            }
        }

        void OnHighSchoolPropertyChanged(object sender, PropertyChangedEventArgs args) {
            if (args.PropertyName == "StudentBody" && presenter.StudentBody != null) {
                this.Groups = new ObservableCollection<StudentGroup>();
                this.Groups.Add(new StudentGroup { Title = "Male" });
                this.Groups.Add(new StudentGroup { Title = "Female" });

                foreach (Student student in presenter.StudentBody.Students)
                    if (student.Sex == "Male")
                        this.Groups[0].Students.Add(student);
                    else
                        this.Groups[1].Students.Add(student);
            }
        }

        public ObservableCollection<StudentGroup> Groups {
            set { SetProperty<ObservableCollection<StudentGroup>>(ref groups, value); }
            get { return groups; }
        }
    }
}
