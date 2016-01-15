using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ch06_MVVM.Commands {
    public interface IDelegateCommand : ICommand {
        void RaiseCanExecuteChanged();
    }
}
