using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingTester.ViewModels {
    public class PersonViewModel : BindableBase {
        private int id;
        public int Id {
            get {
                return id;
            }
            set {
                SetProperty(ref id, value);
                OnPropertyChanged("Id");
            }
        }

        private string firstName;
        public string FirstName {
            get {
                return firstName;
            }
            set {
                SetProperty(ref firstName, value);
                OnPropertyChanged("FirstName");
                OnPropertyChanged("FullName");
                OnPropertyChanged("Message");
            }
        }

        private string lastName;
        public string LastName {
            get {
                return lastName;
            }
            set {
                SetProperty(ref lastName, value);
                OnPropertyChanged("LastName");
                OnPropertyChanged("FullName");
            }
        }

        private string _message;
        public string Message {
            get {
                return string.Format("Hi {0}", FirstName);
            }
            set {
                SetProperty(ref _message, value);
            }
        }

        private string _fullName;
        public string FullName {
            get {
                return string.Format("{0} {1}", FirstName, LastName);
            }
            set {
                SetProperty(ref _fullName, value);
            }
        }

        public PersonViewModel() {
        }
    }
}
