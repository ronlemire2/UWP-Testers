using DataBindingTester.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingTester.Services {
    public interface IPersonService {
        List<Person> GetPersons();
        Person GetPerson(int id);
    }
}
