using DataBindingTester.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingTester.Repositories {
    public interface IPersonRepository {
        List<PersonViewModel> GetPersonVMs();
        PersonViewModel GetPersonVM(int id);
    }
}
