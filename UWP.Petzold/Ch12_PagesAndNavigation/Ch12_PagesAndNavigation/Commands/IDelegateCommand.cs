using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ch12_PagesAndNavigation.Commands {
    public interface IDelegateCommand : ICommand {
        void RaiseCanExecuteChanged();
    }
}
