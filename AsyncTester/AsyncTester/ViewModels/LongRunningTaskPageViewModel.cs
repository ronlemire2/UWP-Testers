using AsyncTester.Services;
using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Windows.Navigation;

namespace AsyncTester.ViewModels {
    public class LongRunningTaskPageViewModel : ViewModelBase {
        ISimplePersonRepository simplePersonRepository;
        public DelegateCommand FemaleNamesCommand { get; set; }

        #region Constructors

        public LongRunningTaskPageViewModel(ISimplePersonRepository simplePersonRepository) {
            this.simplePersonRepository = simplePersonRepository;
            FemaleNamesCommand = new DelegateCommand(ExecuteFemaleNamesCommand, CanExecuteFemaleNamesCommand);
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState) {
            base.OnNavigatedTo(e, viewModelState);
        }

        #endregion

        #region Properties

        private List<SimplePersonViewModel> simplePersonVMs;
        public List<SimplePersonViewModel> SimplePersonVMs {
            get { return simplePersonVMs; }
            set {
                SetProperty<List<SimplePersonViewModel>>(ref simplePersonVMs, value);
            }
        }

        private SimplePersonViewModel selectedSimplePersonVM;
        public SimplePersonViewModel SelectedSimplePersonVM {
            get { return selectedSimplePersonVM; }
            set {
                SetProperty<SimplePersonViewModel>(ref selectedSimplePersonVM, value);
            }
        }


        #endregion



        #region Commands

        private async void ExecuteFemaleNamesCommand() {
            SimplePersonVMs = await simplePersonRepository.GetSimplePersonVMs(Sex.Female, 100);
        }
        private bool CanExecuteFemaleNamesCommand() {
            return true;
        }

        #endregion
    }
}
