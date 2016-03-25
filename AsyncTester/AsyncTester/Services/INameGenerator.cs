using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTester.Services {
    public interface INameGenerator {
        Task<string> GenerateFirstNameAsync(Sex sex);
        Task<string> GenerateLastNameAsync();
        Task<string> GenerateFullNameAsync(Sex sex);

        string GenerateFirstName(Sex sex);
        string GenerateLastName();
        string GenerateFullName(Sex sex);
    }
}
