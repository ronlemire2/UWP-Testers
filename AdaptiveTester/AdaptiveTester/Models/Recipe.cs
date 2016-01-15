using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaptiveTester.Models {
    public class Recipe {
        public string UniqueId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string TileImagePath { get; set; }
        public int PrepTime { get; set; }
        public string Directions { get; set; }
        public ObservableCollection<string> Ingredients { get; set; }
    }
}
