using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ch02_XamlSyntax.Commands {
    public interface IDelegateCommand : ICommand {
        void RaiseCanExecuteChanged();
    }
}
