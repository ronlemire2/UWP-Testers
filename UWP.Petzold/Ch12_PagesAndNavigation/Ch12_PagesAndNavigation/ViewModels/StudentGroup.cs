using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch12_PagesAndNavigation.ViewModels {
    public class StudentGroup : BindableBase {
        string title;
        ObservableCollection<Student> students = new ObservableCollection<Student>();

        public StudentGroup() {
            this.Students = new ObservableCollection<Student>();
        }

        public string Title {
            set { SetProperty<string>(ref title, value); }
            get { return title; }
        }

        public ObservableCollection<Student> Students {
            set { SetProperty<ObservableCollection<Student>>(ref students, value); }
            get { return students; }
        }
    }
}
