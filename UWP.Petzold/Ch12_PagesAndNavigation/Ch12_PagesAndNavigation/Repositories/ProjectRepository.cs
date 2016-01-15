using Ch12_PagesAndNavigation.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch12_PagesAndNavigation.Repositories {
    public class ProjectRepository : IProjectRepository {
        DateTime startDate;
        private ObservableCollection<Activity> activities;

        public ProjectRepository() {
            DateTime.TryParse("1/1/2016", out startDate);
            PopulateActivities();
        }

        public ObservableCollection<Activity> GetActivities() {
            return activities;
        }

        private void PopulateActivities() {
            activities = new ObservableCollection<Activity>();
            activities.Add(new Activity() { Name = "Activity 1", Complete = true, DueDate = startDate.AddDays(4), Project = "Project 1" });
            activities.Add(new Activity() { Name = "Activity 2", Complete = true, DueDate = startDate.AddDays(5), Project = "Project 1" });
            activities.Add(new Activity() { Name = "Activity 3", Complete = false, DueDate = startDate.AddDays(7), Project = "Project 1" });
            activities.Add(new Activity() { Name = "Activity 4", Complete = false, DueDate = startDate.AddDays(9), Project = "Project 1" });
            activities.Add(new Activity() { Name = "Activity 5", Complete = false, DueDate = startDate.AddDays(14), Project = "Project 1" });
            activities.Add(new Activity() { Name = "Activity A", Complete = true, DueDate = startDate.AddDays(2), Project = "Project 2" });
            activities.Add(new Activity() { Name = "Activity B", Complete = false, DueDate = startDate.AddDays(4), Project = "Project 2" });
            activities.Add(new Activity() { Name = "Activity C", Complete = true, DueDate = startDate.AddDays(5), Project = "Project 2" });
            activities.Add(new Activity() { Name = "Activity D", Complete = false, DueDate = startDate.AddDays(9), Project = "Project 2" });
            activities.Add(new Activity() { Name = "Activity E", Complete = false, DueDate = startDate.AddDays(18), Project = "Project 2" });
        }
    }
}
