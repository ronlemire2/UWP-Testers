using AsyncTester.Services;
using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTester.ViewModels {
    public class LocalDataPickerPageViewModel : DataOperationsViewModelBase {
        #region Constructors

        public LocalDataPickerPageViewModel(IPlanetRepository planetRepository) : base(planetRepository) {
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
            try {
                var picker = new Windows.Storage.Pickers.FileOpenPicker();
                picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
                picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                picker.FileTypeFilter.Add(".json");
                Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
                if (file != null) {
                    var data = await Windows.Storage.FileIO.ReadTextAsync(file);
                    Planets = planetRepository.JsonToPlanets(data);
                    Results = BuildResults(Planets);
                }
                else {
                    Results = "Operation cancelled";
                }
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
                var picker = new Windows.Storage.Pickers.FileOpenPicker();
                picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
                picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                picker.FileTypeFilter.Add(".json");
                Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
                if (file != null) {
                    
                }
                else {
                    Results = "Operation cancelled";
                }
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
                var savePicker = new Windows.Storage.Pickers.FileSavePicker();
                savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                savePicker.FileTypeChoices.Add("Json", new List<string>() { ".json" });
                savePicker.SuggestedFileName = "planets";
                Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
                if (file != null) {
                    Planets = planetRepository.ReloadPlanets();
                    var json = planetRepository.PlanetsToJson(Planets);
                    await Windows.Storage.FileIO.WriteTextAsync(file, json);
                }
                else {
                    Results = "Operation cancelled";
                }
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
                var picker = new Windows.Storage.Pickers.FileOpenPicker();
                picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
                picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                picker.FileTypeFilter.Add(".json");
                Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
                if (file != null) {
                    await file.DeleteAsync(Windows.Storage.StorageDeleteOption.PermanentDelete);
                }
                else {
                    Results = "Operation cancelled";
                }
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
