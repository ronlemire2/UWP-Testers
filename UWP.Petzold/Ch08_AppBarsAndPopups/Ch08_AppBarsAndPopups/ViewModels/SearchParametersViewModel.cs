using Ch08_AppBarsAndPopups.Commands;
using Ch08_AppBarsAndPopups.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch08_AppBarsAndPopups.ViewModels {
    public class SearchParametersViewModel : BindableBase {
        public SearchParametersViewModel() {
            OKCommand = new DelegateCommand(ExecuteOK, CanExecuteOKCommand);
            CancelCommand = new DelegateCommand(ExecuteCancel);
        }

        public DialogResult Result { get; private set; }

        public IDelegateCommand OKCommand { get; protected set; }
        public IDelegateCommand CancelCommand { get; protected set; }

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

        private string stateProvinceCode;
        public string StateProvinceCode {
            get { return stateProvinceCode; }
            set {
                SetProperty<string>(ref stateProvinceCode, value);
            }
        }

        private string errorText;
        public string ErrorText {
            get { return errorText; }
            set {
                SetProperty<string>(ref errorText, value);
            }
        }

        async void ExecuteOK(object param) {
            await SomeAsyncOperation();
            Result = DialogResult.OK;
        }

        bool CanExecuteOKCommand(object param) {
            // ContentDialog not subscribing to ICommand.CanExecuteChanged
            return !string.IsNullOrWhiteSpace(FirstName) &&
                !string.IsNullOrWhiteSpace(LastName) &&
                !string.IsNullOrWhiteSpace(StateProvinceCode);
        }

        void ExecuteCancel(object param) {
            Result = DialogResult.Cancel;
        }

        async Task SomeAsyncOperation() {
            await Task.Delay(1000);
        }
    }
}
