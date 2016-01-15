using Ch12_PagesAndNavigation.Models;
using Ch12_PagesAndNavigation.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Ch12_PagesAndNavigation.ViewModels {
    public class ProjectViewModel : BindableBase {
        private ObservableCollection<Activity> activities;

        public ProjectViewModel() : this(new ProjectRepository()) {
        }

        public ProjectViewModel(IProjectRepository projectRepository) {
            Activities = projectRepository.GetActivities();

            ActivitiesCVS = new CollectionViewSource();
            var groups = Activities
                .OrderBy(i => i.Project)
                .GroupBy(i => i.Project)
                .Select(g => new { GroupName = g.Key, Items = g });
            ActivitiesCVS.Source = groups;
            ActivitiesCVS.IsSourceGrouped = true;
            ActivitiesCVS.ItemsPath = new PropertyPath("Items");
            ActivitiesCollectionView = ActivitiesCVS.View;
        }

        public ObservableCollection<Activity> Activities {
            get { return activities; }
            set {
                SetProperty<ObservableCollection<Activity>>(ref activities, value);
            }
        }

        private CollectionViewSource activitiesCVS;
        public CollectionViewSource ActivitiesCVS {
            get { return activitiesCVS; }
            set {
                SetProperty<CollectionViewSource>(ref activitiesCVS, value);
            }
        }

        private ICollectionView activitiesCollectionView;
        public ICollectionView ActivitiesCollectionView {
            get { return activitiesCollectionView; }
            set {
                SetProperty<ICollectionView>(ref activitiesCollectionView, value);
            }
        }
    }
}
