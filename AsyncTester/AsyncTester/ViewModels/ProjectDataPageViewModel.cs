using AsyncTester.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.UI.Popups;

namespace AsyncTester.ViewModels {
    public class ProjectDataPageViewModel : DataOperationsViewModelBase {
        #region Constructors

        public ProjectDataPageViewModel() {
            CreateCommand = new DelegateCommand(ExecuteCreateCommand, CanExecuteCreateCommand);
            ReadCommand = new DelegateCommand(ExecuteReadCommand, CanExecuteReadCommand);
            WriteCommand = new DelegateCommand(ExecuteWriteCommand, CanExecuteWriteCommand);
            DeleteCommand = new DelegateCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
        }

        #endregion


        #region Commands

        // Read
        private async void ExecuteReadCommand() {
            Results = string.Empty;
            Planets = new List<Planet>();

            try {
                // This code from: http://blog.jerrynixon.com/2012/06/windows-8-how-to-read-files-in-winrt.html does not work.
                // Error: "The filename, directory name, or volume label syntax is incorrect. (Exception from HRESULT: 0x8007007B)"
                //var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                //folder = await folder.GetFolderAsync("\\Assets\\Data");
                //var file = await folder.GetFileAsync("planets.json");
                //var data = await Windows.Storage.FileIO.ReadTextAsync(file);

                // http://stackoverflow.com/questions/21500336/how-to-get-file-in-winrt-from-project-path
                var uri = new System.Uri("ms-appx:///Assets/Data/planets.json");
                var file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(uri);
                var data = await Windows.Storage.FileIO.ReadTextAsync(file);

                JsonObject jsonObject = JsonObject.Parse(data);
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


        #endregion
    }
}
