using AsyncTester.Models;
using AsyncTester.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace AsyncTester.Services {
    public class PlanetRepository : IPlanetRepository {
        public List<Planet> Planets { get; set; }

        public PlanetRepository() {
        }

        public List<Planet> GetPlanets() {
            return Planets;
        }

        public Planet GetPlanet(int id) {
            return Planets.Find(p => p.Id == id);
        }

        public int CreatePlanet(Planet planet) {
            Planet newPlanet = new Planet { Id = Planets.Max(p => p.Id) + 1,
                Diameter = planet.Diameter,
                DistanceFromSun = planet.DistanceFromSun,
                Gravity = planet.Gravity,
                ImagePath = planet.ImagePath,
                LengthOfDay = planet.LengthOfDay,
                Mass = planet.Mass,
                MeanTemperature = planet.MeanTemperature,
                Name = planet.Name,
                NumberOfMoons = planet.NumberOfMoons,
                OrbitalPeriod = planet.OrbitalPeriod
            };
            Planets.Add(newPlanet);
            return newPlanet.Id;
        }

        public int UpdatePlanet(Planet planet) {
            Planet planetFound = Planets.Find(p => p.Id == planet.Id);
            planetFound.Diameter = planet.Diameter;
            planetFound.DistanceFromSun = planet.DistanceFromSun;
            planetFound.Gravity = planet.Gravity;
            planetFound.ImagePath = planet.ImagePath;
            planetFound.LengthOfDay = planet.LengthOfDay;
            planetFound.Mass = planet.Mass;
            planetFound.MeanTemperature = planet.MeanTemperature;
            planetFound.Name = planet.Name;
            planetFound.NumberOfMoons = planet.NumberOfMoons;
            planetFound.OrbitalPeriod = planet.OrbitalPeriod;
            return 1;
        }

        public bool DeletePlanet(int id) {
            return Planets.Remove(Planets.Find(p => p.Id == id));
        }

        public List<Planet> ReloadPlanets() {
            // http://nssdc.gsfc.nasa.gov/planetary/factsheet/
            Planets = new List<Planet> {
                new Planet { Id = 1, Name = "Mercury", ImagePath = "/Assets/Planets/Mercury.png",
                    Mass = "0.330", Diameter = "4879", Gravity = "3.7", LengthOfDay = "4222.6",
                    DistanceFromSun = "57.9", OrbitalPeriod = "88.0", MeanTemperature = "167", NumberOfMoons = "0" },

                new Planet { Id = 2, Name = "Venus", ImagePath = "/Assets/Planets/Venus.png",
                    Mass = "4.7", Diameter = "12104", Gravity = "8.9", LengthOfDay = "2802.0",
                    DistanceFromSun = "108.2", OrbitalPeriod = "224.7", MeanTemperature = "464", NumberOfMoons = "0" },

                new Planet { Id = 3, Name = "Earth", ImagePath = "/Assets/Planets/Earth.png",
                    Mass = "5.97", Diameter = "12756", Gravity = "9.8", LengthOfDay = "24.0",
                    DistanceFromSun = "149.6", OrbitalPeriod = "365.2", MeanTemperature = "15", NumberOfMoons = "1" },

                new Planet { Id = 4, Name = "The Moon", ImagePath = "/Assets/Planets/Moon.png",
                    Mass = "0.073", Diameter = "3475", Gravity = "1.6", LengthOfDay = "708.7",
                    DistanceFromSun = "0.384", OrbitalPeriod = "27.3", MeanTemperature = "-20", NumberOfMoons = "0" },

                new Planet { Id = 5, Name = "Mars", ImagePath = "/Assets/Planets/Mars.png",
                    Mass = "0.642", Diameter = "6792", Gravity = "3.7", LengthOfDay = "24.7",
                    DistanceFromSun = "227.9", OrbitalPeriod = "687.0", MeanTemperature = "-65", NumberOfMoons = "2" },

                new Planet { Id = 6, Name = "Jupiter", ImagePath = "/Assets/Planets/Jupiter.png",
                    Mass = "1898.0", Diameter = "142984", Gravity = "23.1", LengthOfDay = "9.9",
                    DistanceFromSun = "778.6", OrbitalPeriod = "4331", MeanTemperature = "-110", NumberOfMoons = "67" },

                new Planet { Id = 7, Name = "Saturn", ImagePath = "/Assets/Planets/Saturn.png",
                    Mass = "568.0", Diameter = "120536", Gravity = "9.0", LengthOfDay = "10.7",
                    DistanceFromSun = "1433.5", OrbitalPeriod = "10747", MeanTemperature = "-140", NumberOfMoons = "62" },

                new Planet { Id = 8, Name = "Uranus", ImagePath = "/Assets/Planets/Uranus.png",
                    Mass = "86.8", Diameter = "51118", Gravity = "8.7", LengthOfDay = "17.2",
                    DistanceFromSun = "2872.5", OrbitalPeriod = "30589", MeanTemperature = "-195", NumberOfMoons = "27" },

                new Planet { Id = 9, Name = "Neptune", ImagePath = "/Assets/Planets/Neptune.png",
                    Mass = "102.0", Diameter = "49528", Gravity = "11.0", LengthOfDay = "16.1",
                    DistanceFromSun = "4495.1", OrbitalPeriod = "59800", MeanTemperature = "-200", NumberOfMoons = "14" },

                new Planet { Id = 10, Name = "Pluto", ImagePath = "/Assets/Planets/Pluto.png",
                    Mass = "0.0146", Diameter = "2370", Gravity = "0.7", LengthOfDay = "153.3",
                    DistanceFromSun = "5906.4", OrbitalPeriod = "90560", MeanTemperature = "-225", NumberOfMoons = "5" },
            };
            return Planets;
        }

        public List<Planet> JsonToPlanets(string json) {
            List<Planet> planets = new List<Planet>();
            planets = JsonConvert.DeserializeObject<List<Planet>>(json);
            return planets;
        }

        public string PlanetsToJson(List<Planet> planets) {
            string json = JsonConvert.SerializeObject(planets,Formatting.Indented);
            return json;
        }
    }
}
