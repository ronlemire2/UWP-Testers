using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemsControlTester.Models {
    public class Author : IEnumerable<BookSku> {

        private static Dictionary<string, Author> authorDictionary = new Dictionary<string, Author>();
        private ObservableCollection<BookSku> bookSkus = new ObservableCollection<BookSku>();

        #region Constructors

        public Author(string name) {
            this.Name = name;
            Author.authorDictionary.Add(this.Name, this);
        }

        #endregion

        
        #region Properties

        public ObservableCollection<BookSku> BookSkus {
            get { return this.bookSkus; }
        }

        public string Name {
            get;
            set;
        }

        public IEnumerator<BookSku> GetEnumerator() {
            return this.BookSkus.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.BookSkus.GetEnumerator();
        }

        #endregion


        #region Methods

        internal static Author GetAuthorByName(string name) {
            Author author;
            Author.authorDictionary.TryGetValue(name, out author);
            return author;
        }

        public void AddBookSku(BookSku bookSku) {
            this.BookSkus.Add(bookSku);
        }

        #endregion
    }
}
