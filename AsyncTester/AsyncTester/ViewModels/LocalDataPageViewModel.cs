using AsyncTester.Models;
using AsyncTester.Services;
using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace AsyncTester.ViewModels {
    public class LocalDataPageViewModel : DataOperationsViewModelBase {
        #region Constructors

        public LocalDataPageViewModel(IPlanetRepository planetRepository) : base(planetRepository) {
            CreateCommand = new DelegateCommand(ExecuteCreateCommand, CanExecuteCreateCommand);
            ReadCommand = new DelegateCommand(ExecuteReadCommand, CanExecuteReadCommand);
            WriteCommand = new DelegateCommand(ExecuteWriteCommand, CanExecuteWriteCommand);
            DeleteCommand = new DelegateCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            Instructions = string.Empty;
        }

        #endregion


        #region Commands

        // Read
        private async void ExecuteReadCommand() {
            Results = string.Empty;
            Planets = new List<Planet>();

            try {
                var folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var file = await folder.GetFileAsync("planets.json");
                var data = await Windows.Storage.FileIO.ReadTextAsync(file);

                Planets = planetRepository.JsonToPlanets(data);
                Results = BuildResults(Planets);

            }
            catch (Exception x) {
                Results = x.Message;
            }
        }
        private bool CanExecuteReadCommand() {
            return true;
        }

        // Write
        private async void ExecuteWriteCommand() {
            try {
                var folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var option = Windows.Storage.CreationCollisionOption.ReplaceExisting;
                var file = await folder.CreateFileAsync("planets.json", option);
                var data = await Windows.Storage.FileIO.ReadTextAsync(file);
                Planets = planetRepository.JsonToPlanets(data);

                Planet planet = Planets.Where(p => p.Name == "Earth" || p.Name == "Zoltan").FirstOrDefault();
                planet.Name = planet.Name == "Earth" ? "Zoltan" : "Earth";

                var json = planetRepository.PlanetsToJson(Planets);
                await Windows.Storage.FileIO.WriteTextAsync(file, json);
            }
            catch (Exception x) {
                Results = x.Message;
            }
        }
        private bool CanExecuteWriteCommand() {
            return true;
        }

        // Create
        private async void ExecuteCreateCommand() {
            try {
                var folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var option = Windows.Storage.CreationCollisionOption.ReplaceExisting;
                var file = await folder.CreateFileAsync("planets.json", option);

                Planets = planetRepository.ReloadPlanets();

                var json = planetRepository.PlanetsToJson(Planets);
                await Windows.Storage.FileIO.WriteTextAsync(file, json);
            }
            catch (Exception x) {
                Results = x.Message;
            }

        }
        private bool CanExecuteCreateCommand() {
            return true;
        }

        // Delete
        private async void ExecuteDeleteCommand() {
            try {
                var folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var file = await folder.GetFileAsync("planets.json");
                await file.DeleteAsync(Windows.Storage.StorageDeleteOption.PermanentDelete);
            }
            catch (Exception x) {
                Results = x.Message;
            }
        }
        private bool CanExecuteDeleteCommand() {
            return true;
        }


        #endregion
    }
}
