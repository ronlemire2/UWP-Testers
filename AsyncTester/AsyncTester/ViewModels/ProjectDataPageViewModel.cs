using AsyncTester.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.UI.Popups;

namespace AsyncTester.ViewModels {
    public class ProjectDataPageViewModel : ViewModelBase {
        public DelegateCommand CreateCommand { get; set; }
        public DelegateCommand ReadCommand { get; set; }
        public DelegateCommand WriteCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        #region Constructors

        public ProjectDataPageViewModel() {
            CreateCommand = new DelegateCommand(ExecuteCreateCommand, CanExecuteCreateCommand);
            ReadCommand = new DelegateCommand(ExecuteReadCommand, CanExecuteReadCommand);
            WriteCommand = new DelegateCommand(ExecuteWriteCommand, CanExecuteWriteCommand);
            DeleteCommand = new DelegateCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);

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

        private string results;
        public string Results {
            get { return results; }
            set {
                SetProperty<string>(ref results, value);
            }
        }

        #endregion

        #region Commands

        // Read
        private async void ExecuteReadCommand() {
            Results = string.Empty;
            Planets = new List<Planet>();

            try {
                // http://stackoverflow.com/questions/21500336/how-to-get-file-in-winrt-from-project-path
                var uri = new System.Uri("ms-appx:///Assets/Data/planets.json");
                var planetsJsonFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(uri);
                var planetsJsonData = await Windows.Storage.FileIO.ReadTextAsync(planetsJsonFile);

                JsonObject jsonObject = JsonObject.Parse(planetsJsonData);
                JsonArray jsonArray = jsonObject["Planets"].GetArray();

                Planet planet;
                foreach (JsonValue planetElement in jsonArray) {
                    JsonObject planetObject = planetElement.GetObject();
                    planet = new Planet {
                        Id = int.Parse(planetObject["Id"].GetString()),
                        Name = planetObject["Name"].GetString(),
                        ImagePath = planetObject["ImagePath"].GetString(),
                        Mass = planetObject["Mass"].GetString(),
                        Diameter = planetObject["Diameter"].GetString(),
                        Gravity = planetObject["Gravity"].GetString(),
                        LengthOfDay = planetObject["LengthOfDay"].GetString(),
                        DistanceFromSun = planetObject["DistanceFromSun"].GetString(),
                        OrbitalPeriod = planetObject["OrbitalPeriod"].GetString(),
                        MeanTemperature = planetObject["MeanTemperature"].GetString(),
                        NumberOfMoons = planetObject["NumberOfMoons"].GetString()
                    };
                    Planets.Add(planet);
                }
                Results = BuildResults(Planets);

            }
            catch (Exception x) {
                string msg = x.Message;
            }
        }
        private bool CanExecuteReadCommand() {
            return true;
        }

        // Create
        private void ExecuteCreateCommand() {
            Results = string.Empty;
            InvalidOperation("Create");
        }
        private bool CanExecuteCreateCommand() {
            return true;
        }

        // Write
        private void ExecuteWriteCommand() {
            Results = string.Empty;
            InvalidOperation("Write");
        }
        private bool CanExecuteWriteCommand() {
            return true;
        }

        // Delete
        private void ExecuteDeleteCommand() {
            Results = string.Empty;
            InvalidOperation("Delete");
        }
        private bool CanExecuteDeleteCommand() {
            return true;
        }

        private async void InvalidOperation(string operation) {
            // Apps don't have write access to their installed directory
            // https://social.msdn.microsoft.com/Forums/en-US/a32d16fd-7855-4fce-9091-8067f712a1b6/access-denied-on-windowsstoragefileiowritetextasync-to-write-a-text-file-in-a-local-application?forum=winappswithhtml5
            MessageDialog msgDialog = new MessageDialog(string.Format("Invalid Operation: {0} Project Data", operation),
                "Apps don't have write access to their installed directory");
            await msgDialog.ShowAsync();
        }

        private string BuildResults(List<Planet> planets) {
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
