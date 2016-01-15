using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DialogsTester.Commands {
    public interface IDelegateCommand : ICommand {
        void RaiseCanExecuteChanged();
    }
}
