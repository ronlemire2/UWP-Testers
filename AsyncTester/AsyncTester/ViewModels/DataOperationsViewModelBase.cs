using AsyncTester.Models;
using AsyncTester.Services;
using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTester.ViewModels {
    public class DataOperationsViewModelBase : ViewModelBase {
        protected IPlanetRepository planetRepository;
        public DelegateCommand CreateCommand { get; set; }
        public DelegateCommand ReadCommand { get; set; }
        public DelegateCommand WriteCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        #region Constructors

        public DataOperationsViewModelBase(IPlanetRepository planetRepository) {
            this.planetRepository = planetRepository;
        }

        #endregion

        #region Properties

        private List<Planet> planets;
        public List<Planet> Planets {
            get { return planets; }
            set {
                SetProperty<List<Planet>>(ref planets, value);
            }
        }

        private string instructions;
        public string Instructions {
            get { return instructions; }
            set {
                SetProperty<string>(ref instructions, value);
            }
        }

        private string results;
        public string Results {
            get { return results; }
            set {
                SetProperty<string>(ref results, value);
            }
        }

        #endregion

        #region Utils

        protected void InvalidOperation(string operation) {
            // Apps don't have write access to their installed directory
            // https://social.msdn.microsoft.com/Forums/en-US/a32d16fd-7855-4fce-9091-8067f712a1b6/access-denied-on-windowsstoragefileiowritetextasync-to-write-a-text-file-in-a-local-application?forum=winappswithhtml5
            Results = string.Format("Invalid Operation: {0}{1}{2} Project Data", operation, Environment.NewLine,
                "Apps don't have write access to their installed directory");
        }

        protected string BuildResults(List<Planet> planets) {
            StringBuilder sb = new StringBuilder();

            foreach (Planet planet in planets) {
                sb.Append(string.Format("Name: {0}", planet.Name));
                sb.AppendLine();
            }

            return sb.ToString();
        }

        #endregion
    }
}
