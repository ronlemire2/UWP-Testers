using Ch12_PagesAndNavigation.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch12_PagesAndNavigation.Repositories {
    public interface IProjectRepository {
        ObservableCollection<Activity> GetActivities();
    }
}
