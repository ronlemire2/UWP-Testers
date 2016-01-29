using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTester.Services {
    public interface IJsonFileService {
        void CreateJsonFile(string fileName, string json);
        void AddJsonObject(string fileName, string json);
        Task<string> ReadAllJsonObjects(string fileName);
        string ReadOneJsonObject(string fileName, int id);
        void EditJsonObject(string fileName, string json);
        void DeleteJsonObject(string fileName, string json);
        void DeleteJsonFile(string fileName);
    }
}
