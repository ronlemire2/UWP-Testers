using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTester.Services {
    public class JsonFileService : IJsonFileService {
        public async void CreateJsonFile(string fileName, string json) {
            var folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var option = Windows.Storage.CreationCollisionOption.ReplaceExisting;
            var file = await folder.CreateFileAsync(fileName, option);
            await Windows.Storage.FileIO.WriteTextAsync(file, json);
        }

        public async void AddJsonObject(string fileName, string json) {
            var folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var option = Windows.Storage.CreationCollisionOption.ReplaceExisting;
            var file = await folder.CreateFileAsync(fileName, option);
            await Windows.Storage.FileIO.WriteTextAsync(file, json);
        }

        public async Task<string> ReadAllJsonObjects(string fileName) {
            var folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var file = await folder.GetFileAsync(fileName);
            var data = await Windows.Storage.FileIO.ReadTextAsync(file);
            return data;
        }

        public string ReadOneJsonObject(string fileName, int id) {
            throw new NotImplementedException();
        }

        public async void EditJsonObject(string fileName, string json) {
            var folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var option = Windows.Storage.CreationCollisionOption.ReplaceExisting;
            var file = await folder.CreateFileAsync(fileName, option);
            await Windows.Storage.FileIO.WriteTextAsync(file, json);
        }

        public async void DeleteJsonObject(string fileName, string json) {
            var folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var option = Windows.Storage.CreationCollisionOption.ReplaceExisting;
            var file = await folder.CreateFileAsync(fileName, option);
            await Windows.Storage.FileIO.WriteTextAsync(file, json);
        }

        public async void DeleteJsonFile(string fileName) {
            var folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var file = await folder.GetFileAsync(fileName);
            await file.DeleteAsync(Windows.Storage.StorageDeleteOption.PermanentDelete);
        }

        public async Task<bool> BackupJson(string json) {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("Json", new List<string>() { ".json" });
            savePicker.SuggestedFileName = "PlanetsBackup";
            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null) {
                await Windows.Storage.FileIO.WriteTextAsync(file, json);
                return true;
            }
            else {
                return false;
            }
        }

        public async Task<string> RestoreJson() {
            string json = string.Empty;

            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".json");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null) {
                json = await Windows.Storage.FileIO.ReadTextAsync(file);
            }

            return json;
        }
    }
}
