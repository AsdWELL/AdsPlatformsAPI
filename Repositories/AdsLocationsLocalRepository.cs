using AdsPlatformsAPI.Exceptions;
using AdsPlatformsAPI.Models;
using AdsPlatformsAPI.Repositories.Interfaces;

namespace AdsPlatformsAPI.Repositories
{
    public class AdsLocationsLocalRepository : IAdsLocationsRepository
    {
        private LocationNode _root;

        public AdsLocationsLocalRepository()
        {
            _root = new LocationNode() { Location = "root" };
        }

        private string[] SplitLocationIntoAreas(string location)
        {
            return location.Split('/', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        }

        private string[] SplitLocationsLineIntoLocations(string locationsLine)
        {
            return locationsLine.Split(',', StringSplitOptions.TrimEntries);
        }

        private (string platformName, string[] locations) SplitLineIntoPlatformNameAndLocations(string line)
        {
            var parts = line.Split(':', 2, StringSplitOptions.TrimEntries);

            if (parts.Length != 2)
                throw new InvalidLineFormatException(line, "Формат строки: <название площадки>:<список локаций через,>");

            var locations = SplitLocationsLineIntoLocations(parts[1]);

            if (locations.Length == 0)
                throw new InvalidLineFormatException(line, "Площадка должна иметь как минимум одну локацию");

            return (parts[0], locations);
        }

        private string[] SplitFileIntoLines(string rawFile)
        {
            return rawFile.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        }

        public Task LoadLocations(string rawFile)
        {
            _root.Childrens.Clear();
            _root.Platforms.Clear();
            
            foreach (var line in SplitFileIntoLines(rawFile))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                
                (var platformName, var locations) = SplitLineIntoPlatformNameAndLocations(line);

                foreach (var location in locations)
                {
                    var locationAreas = SplitLocationIntoAreas(location);

                    if (locationAreas.Length == 0)
                        throw new InvalidLineFormatException(line, $"Площадка {platformName} содержит пустую локацию.");

                    var currentNode = _root;

                    foreach (var area in locationAreas)
                    {
                        if (!currentNode.Childrens.TryGetValue(area, out var node))
                        {
                            node = new LocationNode() { Location = area };
                            currentNode.Childrens.Add(area, node);
                        }

                        currentNode = node;
                    }

                    currentNode.Platforms.Add(platformName);
                }
            }

            return Task.CompletedTask;
        }

        public Task<IEnumerable<string>> GetPlatformsByLocation(string location)
        {
            throw new NotImplementedException();
        }
    }
}
