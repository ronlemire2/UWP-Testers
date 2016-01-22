using ItemsControlTester.Models;
using ItemsControlTester.Services;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace ItemsControlTester.ViewModels {
    // Microsoft Sample - Windows Phone Silverlight to UWP case study:Bookstore2
    // https://msdn.microsoft.com/en-us/library/windows/apps/mt188208.aspx
    public class GridView3PageViewModel : ViewModelBase {
        private ObservableCollection<Author> authors;
        private ObservableCollection<BookSku> bookSkus;

        #region Constructors

        public GridView3PageViewModel() {
            // Establish the invariant of owning a collection of Author and BookSku.
            this.authors = new ObservableCollection<Author>();
            this.bookSkus = new ObservableCollection<BookSku>();

            if (DesignMode.DesignModeEnabled) {
                DataSource.LoadSampleData(ref this.authors, ref this.bookSkus);
            }
            else {
                DataSource.LoadDataFromCloudService(ref this.authors, ref this.bookSkus);
            }
        }

        #endregion


        #region Properties

        public ObservableCollection<Author> Authors { get { return this.authors; } }
        public ObservableCollection<BookSku> BookSkus { get { return this.bookSkus; } }

        public string AppName {
            get {
                return "GridView3Page";
            }
        }

        public string PageTitle {
            get {
                {
                    return "classics";
                }
            }
        }

        #endregion
    }
}
