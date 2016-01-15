using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace DataBindingTester.ViewModels {
    public class ReUseable1PageViewModel : BindableBase {
        private string firstName;
        public string FirstName {
            get { return firstName; }
            set { SetProperty<string>(ref firstName, value); }
        }

        private ObservableCollection<string> names;
        public ObservableCollection<string> Names {
            get { return names; }
            set { SetProperty<ObservableCollection<string>>(ref names, value); }
        }

        private CollectionViewSource namesCvs;
        public CollectionViewSource NamesCvs {
            get { return namesCvs; }
            set { SetProperty<CollectionViewSource>(ref namesCvs, value); }
        }

        private ICollectionView namesCollectionView;
        public ICollectionView NamesCollectionView {
            get { return namesCollectionView; }
            set { SetProperty<ICollectionView>(ref namesCollectionView, value); }
        }
    }
}
