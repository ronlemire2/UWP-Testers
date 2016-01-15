using DataBindingTester.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingTester.Services {
    public class PersonService : IPersonService {
        public List<Person> GetPersons() {
            List<Person> persons = new List<Person>();
            persons.Add(new Person { Id = 1, FirstName = "Spider", LastName = "Caile" });
            persons.Add(new Person { Id = 2, FirstName = "Clara", LastName = "Caile" });
            return persons;
        }

        public Person GetPerson(int id) {
            Person person = GetPersons().Where(p => p.Id == id).SingleOrDefault();
            return person;
        }
    }
}
