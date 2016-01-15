using DataBindingTester.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace DataBindingTester.ViewModels {
    public class EventBindingPageViewModel {
        public DelegateCommand ListBoxSelectionChangedCommand { get; set; }

        public EventBindingPageViewModel() {
            ListBoxSelectionChangedCommand = new DelegateCommand(ExecuteListBoxSelectionChangedCommand, CanExecuteListBoxSelectionChangedCommand);
        }


        private async void ExecuteListBoxSelectionChangedCommand(object args) {
            MessageDialog msgDialog = new MessageDialog("SelectionChanged");
            await msgDialog.ShowAsync();
        }

        private bool CanExecuteListBoxSelectionChangedCommand(object args) {
            return true;
        }
    }
}
