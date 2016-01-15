using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingTester.Models {
    public class Author : IEnumerable<BookSku> {
        #region fields
        private static Dictionary<string, Author> authorDictionary = new Dictionary<string, Author>();
        private ObservableCollection<BookSku> bookSkus = new ObservableCollection<BookSku>();
        #endregion fields

        #region properties
        public ObservableCollection<BookSku> BookSkus { get { return this.bookSkus; } }
        public string Name { get; set; }
        #endregion properties

        #region constructors
        public Author(string name) {
            this.Name = name;
            Author.authorDictionary.Add(this.Name, this);
        }
        #endregion constructors

        #region methods
        internal static Author GetAuthorByName(string name) {
            Author author;
            Author.authorDictionary.TryGetValue(name, out author);
            return author;
        }

        public void AddBookSku(BookSku bookSku) {
            this.BookSkus.Add(bookSku);
        }
        #endregion methods

        #region IEnumerable<BookSku>
        public IEnumerator<BookSku> GetEnumerator() {
            return this.BookSkus.GetEnumerator();
        }
        #endregion IEnumerable<BookSku>

        #region IEnumerable
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.BookSkus.GetEnumerator();
        }
        #endregion IEnumerable
    }
}
