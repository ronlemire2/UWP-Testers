using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch12_PagesAndNavigation.Models {
    public class Activity {
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public bool Complete { get; set; }
        public string Project { get; set; }
    }
}
