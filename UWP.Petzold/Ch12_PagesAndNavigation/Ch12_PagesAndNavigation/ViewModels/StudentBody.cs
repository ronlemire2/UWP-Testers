using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch12_PagesAndNavigation.ViewModels {
    public class StudentBody : BindableBase {
        string school;
        ObservableCollection<Student> students = new ObservableCollection<Student>();

        public string School {
            set { SetProperty<string>(ref school, value); }
            get { return school; }
        }

        public ObservableCollection<Student> Students {
            set { SetProperty<ObservableCollection<Student>>(ref students, value); }
            get { return students; }
        }
    }
}
