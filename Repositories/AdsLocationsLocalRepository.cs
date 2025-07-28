using AdsPlatformsAPI.Exceptions;
using AdsPlatformsAPI.Models;
using AdsPlatformsAPI.Repositories.Interfaces;

namespace AdsPlatformsAPI.Repositories
{
    public partial class AdsLocationsLocalRepository : IAdsLocationsRepository
    {
        private LocationNode _root;

        public AdsLocationsLocalRepository()
        {
            _root = new LocationNode() { Location = "root" };
        }

        [System.Text.RegularExpressions.GeneratedRegex(@"^(/\w+)+$")]
        private static partial System.Text.RegularExpressions.Regex LocationsRegex();

        private string[] SplitLocationIntoAreas(string location)
        {
            return location.Split('/', StringSplitOptions.TrimEntries);
        }

        private string[] SplitLocationsLineIntoLocations(string locationsLine)
        {
            return locationsLine.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
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
                    if (!LocationsRegex().IsMatch(location))
                        throw new InvalidLineFormatException(line, $"Неверный формат локации для {platformName}. Формат: /локация/локация");

                    var locationAreas = SplitLocationIntoAreas(location);

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
