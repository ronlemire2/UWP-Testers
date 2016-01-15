using DataBindingTester.Commands;
using DataBindingTester.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Data;

namespace DataBindingTester.ViewModels {
    public class CVSPageViewModel : BindableBase {
        private IPersonRepository _personRepository;

        // Constructors
        public CVSPageViewModel(IPersonRepository personRepository) {
            _personRepository = personRepository;
        }

        public CVSPageViewModel() : this(new PersonRepository()) {
            FamilyMember = _personRepository.GetPersonVM(1);
            FamilyMembers = new ObservableCollection<PersonViewModel>(_personRepository.GetPersonVMs());
            ClickMeCommand = new DelegateCommand(ExecuteClickMe);
        }

        // Properties
        private ObservableCollection<PersonViewModel> _familyMembers;
        public ObservableCollection<PersonViewModel> FamilyMembers {
            get { return _familyMembers; }
            set { _familyMembers = value; }
        }

        private PersonViewModel _familyMember;
        public PersonViewModel FamilyMember {
            get { return _familyMember; }
            set { _familyMember = value; }
        }

        private PersonViewModel _selectedFamilyMember;
        public PersonViewModel SelectedFamilyMember {
            get { return _selectedFamilyMember; }
            set {
                SetProperty(ref _selectedFamilyMember, value);
            }
        }

        // Commands
        public IDelegateCommand ClickMeCommand { get; set; }

        async void ExecuteClickMe(object param) {
            if (SelectedFamilyMember != null) {
                var messageDialog = new MessageDialog(SelectedFamilyMember.FullName);
                await messageDialog.ShowAsync();
            }
        }
    }
}
