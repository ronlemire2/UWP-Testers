using AsyncTester.Models;
using AsyncTester.Services;
using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Prism.Windows.Navigation;
using AsyncTester.Views;
using Windows.UI.Popups;

namespace AsyncTester.ViewModels {
    public class LocalDataPageViewModel : ViewModelBase {
        const string JSON_FILENAME = "planets.json";
        protected IPlanetRepository planetRepository;
        protected IJsonFileService jsonFileService;
        public DelegateCommand CreateFileCommand { get; set; }
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand ReadCommand { get; set; }
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand DeleteFileCommand { get; set; }
        public DelegateCommand BackupFileCommand { get; set; }
        public DelegateCommand RestoreFileCommand { get; set; }

        #region Constructors

        public LocalDataPageViewModel(IPlanetRepository planetRepository, IJsonFileService jsonFileService) {
            this.planetRepository = planetRepository;
            this.jsonFileService = jsonFileService;
            CreateFileCommand = new DelegateCommand(ExecuteCreateFileCommand, CanExecuteCreateFileCommand);
            AddCommand = new DelegateCommand(ExecuteAddCommand, CanExecuteAddCommand);
            ReadCommand = new DelegateCommand(ExecuteReadCommand, CanExecuteReadCommand);
            EditCommand = new DelegateCommand(ExecuteEditCommand, CanExecuteEditCommand);
            DeleteCommand = new DelegateCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            DeleteFileCommand = new DelegateCommand(ExecuteDeleteFileCommand, CanExecuteDeleteFileCommand);
            BackupFileCommand = new DelegateCommand(ExecuteBackupFileCommand, CanExecuteBackupFileCommand);
            RestoreFileCommand = new DelegateCommand(ExecuteRestoreFileCommand, CanExecuteRestoreFileCommand);
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState) {
            base.OnNavigatedTo(e, viewModelState);
        }

        #endregion

        #region Properties

        private ObservableCollection<PlanetViewModel> planetVMs;
        public ObservableCollection<PlanetViewModel> PlanetVMs {
            get { return planetVMs; }
            set {
                SetProperty<ObservableCollection<PlanetViewModel>>(ref planetVMs, value);
            }
        }

        private PlanetViewModel selectedPlanetVM;
        public PlanetViewModel SelectedPlanetVM {
            get { return selectedPlanetVM; }
            set {
                SetProperty<PlanetViewModel>(ref selectedPlanetVM, value);
                Results = SelectedPlanetVM.Name;
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


        /// <summary>
        /// Create an empty planets.json file in Local Data folder
        /// </summary>
        private void ExecuteCreateFileCommand() {
            try {
                List<Planet> planets = new List<Planet>();
                var json = planetRepository.PlanetsToJson(planets);
                jsonFileService.CreateJsonFile(JSON_FILENAME, json);
            }
            catch (Exception x) {
                Results = x.Message;
            }
        }
        private bool CanExecuteCreateFileCommand() {
            return true;
        }

        /// <summary>
        /// Add new Planet then write all planets to Local Folder planets.json
        /// </summary>
        private async void ExecuteAddCommand() {
            SelectedPlanetVM = new PlanetViewModel();
            LocalDataAddDialog addDialog = new LocalDataAddDialog();
            addDialog.DataContext = this.SelectedPlanetVM;
            try {
                var result = await addDialog.ShowAsync();
                if (result == Windows.UI.Xaml.Controls.ContentDialogResult.Primary) {
                    if (PlanetVMs == null) {
                        PlanetVMs = new ObservableCollection<PlanetViewModel>();
                    }
                    PlanetVMs.Add(SelectedPlanetVM);
                    List<Planet> planets = MapViewModelsToPlanets(PlanetVMs.ToList());
                    var json = planetRepository.PlanetsToJson(planets);
                    jsonFileService.AddJsonObject(JSON_FILENAME, json);
                }
                else {
                    Results = "Operation canceled";
                }
            }
            catch (Exception x) {
                Results = x.Message;
            }
        }
        private bool CanExecuteAddCommand() {
            return true;
        }
        /// <summary>
        /// Read all Planets from Local Folder planets.json into ListView.
        /// </summary>
        private async void ExecuteReadCommand() {
            Results = string.Empty;
            try {
                string data = await jsonFileService.ReadAllJsonObjects(JSON_FILENAME);
                List<Planet> planets = planetRepository.JsonToPlanets(data);
                PlanetVMs = new ObservableCollection<PlanetViewModel>(MapPlanetsToViewModels(planets));
                SelectedPlanetVM = PlanetVMs[0];
            }
            catch (Exception x) {
                Results = x.Message;
            }
        }
        private bool CanExecuteReadCommand() {
            return true;
        }

        /// <summary>
        /// Change the Selected PlanetViewModel, then write all Planets to Local Folder planets.json.
        /// This allows all properties to be changed even Planet.Id.
        /// </summary>
        private async void ExecuteEditCommand() {
            try {
                LocalDataEditDialog editDialog = new LocalDataEditDialog();
                editDialog.DataContext = this.SelectedPlanetVM;
                var result = await editDialog.ShowAsync();
                if (result == Windows.UI.Xaml.Controls.ContentDialogResult.Primary) {
                    List<Planet> planets = MapViewModelsToPlanets(PlanetVMs.ToList());
                    var json = planetRepository.PlanetsToJson(planets);
                    jsonFileService.EditJsonObject(JSON_FILENAME, json);
                }
            }
            catch (Exception x) {
                Results = x.Message;
            }
        }
        private bool CanExecuteEditCommand() {
            return true;
        }


        /// <summary>
        /// Remove SelectedViewModel then write all Planets to Local Folder planets.json
        /// </summary>
        private async void ExecuteDeleteCommand() {
            try {
                LocalDataDeleteDialog deleteDialog = new LocalDataDeleteDialog();
                deleteDialog.DataContext = this.SelectedPlanetVM;
                var result = await deleteDialog.ShowAsync();
                if (result == Windows.UI.Xaml.Controls.ContentDialogResult.Primary) {
                    PlanetVMs.Remove(SelectedPlanetVM);
                    List<Planet> planets = MapViewModelsToPlanets(PlanetVMs.ToList());
                    var json = planetRepository.PlanetsToJson(planets);
                    jsonFileService.DeleteJsonObject(JSON_FILENAME, json);
                }
                else {
                    Results = "Operation canceled";
                }
            }
            catch (Exception x) {
                Results = x.Message;
            }
        }
        private bool CanExecuteDeleteCommand() {
            return true;
        }

        /// <summary>
        /// Delete planets.json file in Local Data folder
        /// </summary>
        private async void ExecuteDeleteFileCommand() {
            var dialog = new MessageDialog(string.Format("Delete planets.json?"));
            dialog.Title = "Are you sure?";
            dialog.Commands.Add(new UICommand { Label = "Delete", Id = 1 });
            dialog.Commands.Add(new UICommand { Label = "Cancel", Id = 0 });
            var res = await dialog.ShowAsync();
            if ((int)res.Id == 1) {
                try {
                    jsonFileService.DeleteJsonFile(JSON_FILENAME);
                    PlanetVMs = null;
                }
                catch (Exception x) {
                    Results = x.Message;
                }
            }
            else {
                Results = "Operation canceled";
            }

        }
        private bool CanExecuteDeleteFileCommand() {
            return true;
        }

        /// <summary>
        /// Delete planets.json file in Local Data folder
        /// </summary>
        private async void ExecuteBackupFileCommand() {
            string errMsg = string.Empty;
            bool success = false;
            try {
                string json = await jsonFileService.ReadAllJsonObjects(JSON_FILENAME);
                success = await jsonFileService.BackupJson(json);
            }
            catch (Exception x) {
                errMsg = x.Message;
            }
            finally {
                Results = errMsg == string.Empty ? success.ToString() : errMsg;
            }
        }
        private bool CanExecuteBackupFileCommand() {
            return true;
        }

        /// <summary>
        /// Delete planets.json file in Local Data folder
        /// </summary>
        private async void ExecuteRestoreFileCommand() {
            string json = await jsonFileService.RestoreJson();
            jsonFileService.CreateJsonFile(JSON_FILENAME, json);
        }
        private bool CanExecuteRestoreFileCommand() {
            return true;
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

        private List<PlanetViewModel> MapPlanetsToViewModels(List<Planet> planets) {
            List<PlanetViewModel> vms = new List<PlanetViewModel>();

            PlanetViewModel vm;
            foreach (Planet p in planets) {
                vm = new PlanetViewModel();
                vm.Id = p.Id.ToString();
                vm.Name = p.Name;
                vm.ImagePath = p.ImagePath;
                vm.Mass = p.Mass;
                vm.Diameter = p.Diameter;
                vm.Gravity = p.Gravity;
                vm.LengthOfDay = p.LengthOfDay;
                vm.DistanceFromSun = p.DistanceFromSun;
                vm.OrbitalPeriod = p.OrbitalPeriod;
                vm.MeanTemperature = p.MeanTemperature;
                vm.NumberOfMoons = p.NumberOfMoons;
                vms.Add(vm);
            }

            return vms;
        }

        private List<Planet> MapViewModelsToPlanets(List<PlanetViewModel> vms) {
            List<Planet> planets = new List<Planet>();

            Planet p;
            foreach (PlanetViewModel vm in vms) {
                p = new Planet();
                p.Id = int.Parse(vm.Id);
                p.Name = vm.Name;
                p.ImagePath = vm.ImagePath;
                p.Mass = vm.Mass;
                p.Diameter = vm.Diameter;
                p.Gravity = vm.Gravity;
                p.LengthOfDay = vm.LengthOfDay;
                p.DistanceFromSun = vm.DistanceFromSun;
                p.OrbitalPeriod = vm.OrbitalPeriod;
                p.MeanTemperature = vm.MeanTemperature;
                p.NumberOfMoons = vm.NumberOfMoons;
                planets.Add(p);
            }

            return planets;
        }




        #endregion
    }
}
