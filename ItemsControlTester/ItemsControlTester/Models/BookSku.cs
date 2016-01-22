using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace ItemsControlTester.Models {
    public class BookSku {
        #region properties
        public Author Author { get; private set; }

        public string AuthorName { get { return this.Author != null ? this.Author.Name : "Anonymous"; } }

        public ImageSource CoverImage {
            get {
                // this.CoverImagePath contains a path of the form "/Assets/CoverImages/one.png".
                return new BitmapImage(new Uri(new Uri("ms-appx://"), this.CoverImagePath));
            }
        }

        public string CoverImagePath { get; private set; }

        public string Title { get; private set; }
        #endregion properties

        #region constructors
        public BookSku(string title, string authorName, string coverImagePath) {
            this.Title = title;
            this.Author = Author.GetAuthorByName(authorName);
            if (this.Author != null) {
                this.Author.AddBookSku(this);
            }
            this.CoverImagePath = coverImagePath;
        }
        #endregion constructors
    }
}
