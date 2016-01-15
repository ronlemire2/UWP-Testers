using DataBindingTester.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

/// <Acknowledgment>
/// https://msdn.microsoft.com/en-us/library/windows/apps/mt210946.aspx  (Bookstore2 link)
/// </Acknowledgment>

namespace DataBindingTester.ViewModels {
    public class SemanticZoom1PageViewModel : BindableBase {
        #region fields
        private ObservableCollection<Author> authors;
        private ObservableCollection<BookSku> bookSkus;
        #endregion fields

        #region properties
        private string appName;
        public string AppName {
            get { return appName; }
            set {
                SetProperty<string>(ref appName, value);
            }
        }

        public ObservableCollection<Author> Authors { get { return this.authors; } }

        public ObservableCollection<BookSku> BookSkus { get { return this.bookSkus; } }

        public string PageTitle {
            get {
                {
                    return "classics";
                }
            }
        }
        #endregion properties


        #region constructors
        public SemanticZoom1PageViewModel() {
            // Establish the invariant of owning a collection of Author and BookSku.
            this.authors = new ObservableCollection<Author>();
            this.bookSkus = new ObservableCollection<BookSku>();

            if (DesignMode.DesignModeEnabled) {
                LoadSampleData(ref this.authors, ref this.bookSkus);
            }
            else {
                LoadDataFromCloudService(ref this.authors, ref this.bookSkus);
            }
        }
        #endregion constructors

        #region methods
        public static void LoadSampleData(ref ObservableCollection<Author> authors, ref ObservableCollection<BookSku> bookSkus) {
            authors.Add(new Author("Austen, Jane"));
            authors.Add(new Author("Burnett, Frances Hodgson"));
            authors.Add(new Author("Dickens, Charles"));
            authors.Add(new Author("Mitchell, Margaret"));
            authors.Add(new Author("Steinbeck, John"));
            authors.Add(new Author("Tolkien, J.R.R."));

            bookSkus.Add(new BookSku("Emma", "Austen, Jane", "/Assets/CoverImages/three.png"));
            bookSkus.Add(new BookSku("Mansfield Park", "Austen, Jane", "/Assets/CoverImages/six.png"));
            bookSkus.Add(new BookSku("Persuasion", "Austen, Jane", "/Assets/CoverImages/four.png"));
            bookSkus.Add(new BookSku("Pride and Prejudice", "Austen, Jane", "/Assets/CoverImages/five.png"));
            bookSkus.Add(new BookSku("Sense and Sensibility", "Austen, Jane", "/Assets/CoverImages/seven.png"));
            bookSkus.Add(new BookSku("The Secret Garden", "Burnett, Frances Hodgson", "/Assets/CoverImages/five.png"));
            bookSkus.Add(new BookSku("A Christmas Carol", "Dickens, Charles", "/Assets/CoverImages/one.png"));
            bookSkus.Add(new BookSku("David Copperfield", "Dickens, Charles", "/Assets/CoverImages/three.png"));
            bookSkus.Add(new BookSku("Great Expectations", "Dickens, Charles", "/Assets/CoverImages/five.png"));
            bookSkus.Add(new BookSku("Little Dorrit", "Dickens, Charles", "/Assets/CoverImages/five.png"));
            bookSkus.Add(new BookSku("Oliver Twist", "Dickens, Charles", "/Assets/CoverImages/two.png"));
            bookSkus.Add(new BookSku("Gone With The Wind", "Mitchell, Margaret", "/Assets/CoverImages/four.png"));
            bookSkus.Add(new BookSku("East of Eden", "Steinbeck, John", "/Assets/CoverImages/two.png"));
            bookSkus.Add(new BookSku("The Fellowship Of The Ring", "Tolkien, J.R.R.", "/Assets/CoverImages/one.png"));
            bookSkus.Add(new BookSku("The Hobbit", "Tolkien, J.R.R.", "/Assets/CoverImages/one.png"));
            bookSkus.Add(new BookSku("The Return Of The King", "Tolkien, J.R.R.", "/Assets/CoverImages/two.png"));
            bookSkus.Add(new BookSku("The Two Towers", "Tolkien, J.R.R.", "/Assets/CoverImages/six.png"));
        }

        public static void LoadDataFromCloudService(ref ObservableCollection<Author> authors, ref ObservableCollection<BookSku> bookSkus) {
            // In this simple app, we'll simulate real-world data access by loading sample data.
            LoadSampleData(ref authors, ref bookSkus);
        }
        #endregion methods
    }
}
