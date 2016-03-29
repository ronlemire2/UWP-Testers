using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTester.ViewModels {
    public class CalcFactorialPageViewModel : ViewModelBase {
        public DelegateCommand GoCommand { get; set; }

        public CalcFactorialPageViewModel() {
            GoCommand = new DelegateCommand(ExecuteGoCommand, CanExecuteGoCommand);
        }

        #region Properties

        private string nString;
        public string NString {
            get {
                return nString;
            }

            set {
                SetProperty<string>(ref nString, value);
            }
        }

        private string answer;
        public string Answer {
            get {
                return answer;
            }

            set {
                SetProperty<string>(ref answer, value);
            }
        }

        #endregion

        #region Commands

        private void ExecuteGoCommand() {
            Answer = CalcFactorial(int.Parse(NString)).ToString();
        }

        private bool CanExecuteGoCommand() {
            return true;
        }

        #endregion

        private BigInteger CalcFactorial(int num) {
            BigInteger result = 1;

            for (int i = 1; i <= num; i++) {
                result *= i;
            }

            return result;
        }
    }
}
