using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTester.Models {
    public class Planet {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Mass { get; set; }               // 10*24 kg
        public string Diameter { get; set; }           // km
        public string Gravity { get; set; }            // m/s2
        public string LengthOfDay { get; set; }        // hours
        public string DistanceFromSun { get; set; }    // 10*6 km
        public string OrbitalPeriod { get; set; }      // days
        public string MeanTemperature { get; set; }    // C
        public string NumberOfMoons { get; set; }      // units    
    }
}
