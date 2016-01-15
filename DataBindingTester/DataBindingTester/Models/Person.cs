using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingTester.Models {
    public class Person {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Person() {
            Id = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
        }

        public Person(int id, string firstName, string lastName) {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
