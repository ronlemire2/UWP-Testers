using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ch12_PagesAndNavigation.ViewModels {
    public class Student : BindableBase {
        string fullName, firstName, middleName, lastName, sex, photoFilename;
        double gradePointAverage;

        public string FullName {
            set { SetProperty<string>(ref fullName, value); }
            get { return fullName; }
        }

        public string FirstName {
            set { SetProperty<string>(ref firstName, value); }
            get { return firstName; }
        }

        public string MiddleName {
            set { SetProperty<string>(ref middleName, value); }
            get { return middleName; }
        }

        public string LastName {
            set { SetProperty<string>(ref lastName, value); }
            get { return lastName; }
        }

        public string Sex {
            set { SetProperty<string>(ref sex, value); }
            get { return sex; }
        }

        public string PhotoFilename {
            set { SetProperty<string>(ref photoFilename, value); }
            get { return photoFilename; }
        }

        public double GradePointAverage {
            set { SetProperty<double>(ref gradePointAverage, value); }
            get { return gradePointAverage; }
        }
    }
}
