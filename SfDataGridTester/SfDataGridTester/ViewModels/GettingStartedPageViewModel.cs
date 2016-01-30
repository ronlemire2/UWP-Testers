using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Windows.Navigation;
using SfDataGridTester.Models;
using SfDataGridTester.Services;

namespace SfDataGridTester.ViewModels {
    public class GettingStartedPageViewModel : ViewModelBase {

        #region Constructors

        public GettingStartedPageViewModel() {

        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState) {
            OrderVMs = new OrderRepository().GetOrderVMs(2000);

            base.OnNavigatedTo(e, viewModelState);
        }

        #endregion

        #region Properties

        private List<OrderViewModel> orderVMs;
        public List<OrderViewModel> OrderVMs {
            get { return orderVMs; }
            set { orderVMs = value; }
        }

        #endregion
    }

}
