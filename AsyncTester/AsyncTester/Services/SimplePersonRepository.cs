using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncTester.ViewModels;

namespace AsyncTester.Services {
    public class SimplePersonRepository : ISimplePersonRepository {
        INameGenerator nameGenerator;

        public SimplePersonRepository(INameGenerator nameGenerator) {
            this.nameGenerator = nameGenerator;
        }

        public async Task<List<SimplePersonViewModel>> GetSimplePersonVMs(Sex sex, int count) {
            List<SimplePersonViewModel> simplePersonVMs = new List<SimplePersonViewModel>();
            SimplePersonViewModel vm;

            for (int i = 0; i < count; i++) {
                vm = new SimplePersonViewModel();
                vm.FirstName = await nameGenerator.GenerateFirstNameAsync(sex);
                vm.LastName = await nameGenerator.GenerateLastNameAsync();
                vm.FullName = string.Format("{0} {1}", vm.FirstName, vm.LastName);
                simplePersonVMs.Add(vm);
            }

            return simplePersonVMs;
        }
    }
}
