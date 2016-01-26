using AsyncTester.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTester.Services {
    public interface IPlanetRepository {
        List<Planet> ReloadPlanets();
        List<Planet> GetPlanets();
        Planet GetPlanet(int id);
        int CreatePlanet(Planet planet);
        int UpdatePlanet(Planet planet);
        bool DeletePlanet(int id);
        List<Planet> JsonToPlanets(string json);
        string PlanetsToJson(List<Planet> planets);
    }
}
