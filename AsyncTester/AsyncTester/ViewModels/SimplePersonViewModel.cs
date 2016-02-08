using Prism.Windows.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTester.ViewModels {
    public class SimplePersonViewModel : ValidatableBindableBase {
        private string firstName;
        public string FirstName {
            get { return firstName; }
            set {
                SetProperty<string>(ref firstName, value);
            }
        }

        private string lastName;
        public string LastName {
            get { return lastName; }
            set {
                SetProperty<string>(ref lastName, value);
            }
        }

        private string fullName;
        public string FullName {
            get { return fullName; }
            set {
                SetProperty<string>(ref fullName, value);
            }
        }

    }
}
