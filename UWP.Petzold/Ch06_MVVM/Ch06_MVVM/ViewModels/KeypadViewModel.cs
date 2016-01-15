using Ch06_MVVM.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch06_MVVM.ViewModels {
    public class KeypadViewModel : BindableBase {
        string inputString = string.Empty;
        string displayText = string.Empty;
        char[] specialChars = { '*', '#' };

        public IDelegateCommand AddCharacterCommand { get; protected set; }
        public IDelegateCommand DeleteCharacterCommand { get; protected set; }

        public KeypadViewModel() {
            AddCharacterCommand = new DelegateCommand(ExecuteAddCharacter);
            DeleteCharacterCommand = new DelegateCommand(ExecuteDeleteCharacter, CanExecuteDeleteCharacter);
        }

        public string InputString {
            get { return inputString; }
            protected set {
                //bool previousCanExecuteDeleteChar = CanExecuteDeleteCharacter(null);
                if (SetProperty<string>(ref inputString, value)) {
                    DisplayText = FormatText(inputString);
                    //if (previousCanExecuteDeleteChar != CanExecuteDeleteCharacter(null)) {
                        DeleteCharacterCommand.RaiseCanExecuteChanged();
                    //
                }
            }
        }

        public string DisplayText {
            get { return displayText; }
            protected set {
                SetProperty<string>(ref displayText, value);
            }
        }

        void ExecuteAddCharacter(object param) {
            InputString += param as string;
        }

        void ExecuteDeleteCharacter(object param) {
            InputString = InputString.Substring(0, InputString.Length - 1);
        }

        bool CanExecuteDeleteCharacter(object param) {
            return InputString.Length > 0;
        }

        string FormatText(string str) {
            bool hasNonNumbers = str.IndexOfAny(specialChars) != -1;
            string formatted = str;

            if (hasNonNumbers || str.Length < 4 || str.Length > 10) {

            }
            else if (str.Length < 8) {
                formatted = string.Format("{0}-{1}", str.Substring(0, 3), str.Substring(3));
            }
            else {
                formatted = string.Format("({0}) {1}-{2}", str.Substring(0, 3), str.Substring(3, 3), str.Substring(6));
            }
            return formatted;
        }
    }
}
