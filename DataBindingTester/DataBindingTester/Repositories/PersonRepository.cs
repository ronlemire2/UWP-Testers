using DataBindingTester.Models;
using DataBindingTester.Services;
using DataBindingTester.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingTester.Repositories {
    public class PersonRepository : IPersonRepository {
        private IPersonService _personService;

        public PersonRepository(IPersonService personService) {
            _personService = personService;
        }

        public PersonRepository() : this(new PersonService()) {

        }

        public List<PersonViewModel> GetPersonVMs() {
            List<PersonViewModel> personVMs = new List<PersonViewModel>();
            PersonViewModel personVM;

            List<Person> persons = _personService.GetPersons();
            foreach (Person person in persons) {
                personVM = new PersonViewModel();
                personVM.Id = person.Id;
                personVM.FirstName = person.FirstName;
                personVM.LastName = person.LastName;
                personVMs.Add(personVM);
            }

            return personVMs;
        }

        public PersonViewModel GetPersonVM(int id) {
            PersonViewModel personVM = new PersonViewModel();
            Person person;

            person = _personService.GetPerson(id);
            personVM.Id = person.Id;
            personVM.FirstName = person.FirstName;
            personVM.LastName = person.LastName;

            return personVM;
        }

    }
}
