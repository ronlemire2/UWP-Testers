using ItemsControlTester.Models;
using ItemsControlTester.Services;
using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace ItemsControlTester.ViewModels {
    public class GridView1PageViewModel : ViewModelBase {
        public DelegateCommand GridViewSelectionChanged { get; set; }

        #region Constructors

        public GridView1PageViewModel() {
            FeedService fs = new FeedService();
            FeedsByLetter = fs.GetFeedGroupsByLetter();
            GridViewSelectionChanged = new DelegateCommand(ExecuteGridViewSelectionChanged, CanExecuteGridViewSelectionChanged);
        }

        #endregion

        #region Properties

        private IEnumerable<object> feedsByLetter;
        public IEnumerable<object> FeedsByLetter {
            get { return feedsByLetter; }
            set {
                SetProperty<IEnumerable<object>>(ref feedsByLetter, value);
            }
        }

        private Feed selectedFeed;
        public Feed SelectedFeed {
            get { return selectedFeed; }
            set {
                SetProperty<Feed>(ref selectedFeed, value);
            }
        }

        private string appName;
        public string AppName {
            get { return appName; }
            set {
                SetProperty<string>(ref appName, value);
            }
        }

        #endregion

        #region Commands

        private async void ExecuteGridViewSelectionChanged() {
            MessageDialog dialog = new MessageDialog(SelectedFeed.FeedName);
            await dialog.ShowAsync();
        }
        private bool CanExecuteGridViewSelectionChanged() {
            return true;
        }

        #endregion
    }
}
