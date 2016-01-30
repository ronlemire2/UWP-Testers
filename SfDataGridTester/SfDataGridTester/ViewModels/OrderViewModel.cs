using Prism.Windows.Mvvm;
using Prism.Windows.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SfDataGridTester.ViewModels {
    public class OrderViewModel : ValidatableBindableBase {
        private int orderId;
        public int OrderId {
            get { return orderId; }
            set { SetProperty<int>(ref orderId, value); }

        }

        private DateTime orderDate;
        public DateTime OrderDate {
            get { return orderDate; }
            set { SetProperty<DateTime>(ref orderDate, value); }
        }

        private string customerId;
        public string CustomerId {
            get { return customerId; }
            set { SetProperty<string>(ref customerId, value); }
        }

    }
}
