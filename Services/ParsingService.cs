using AdsPlatformsAPI.Exceptions;
using AdsPlatformsAPI.Models;
using AdsPlatformsAPI.Services.Interfaces;

namespace AdsPlatformsAPI.Services
{
    public partial class ParsingService : IParsingService
    {
        private string _currentLine;
        
        [System.Text.RegularExpressions.GeneratedRegex(@"^(/\w+)+$")]
        private static partial System.Text.RegularExpressions.Regex LocationsRegex();

        private void CheckLocationStringFormat(string locationString)
        {
            if (!LocationsRegex().IsMatch(locationString))
                throw new InvalidLineFormatException(_currentLine, $"Неверный формат локации. Формат: /локация/локация");
        }
        
        private string[] SplitLocationIntoAreas(string location)
        {
            CheckLocationStringFormat(location);

            return location.Split('/', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        }

        private string[] SplitLocationsLineIntoLocations(string locationsLine)
        {
            var locations = locationsLine.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            if (locations.Length == 0)
                throw new InvalidLineFormatException(_currentLine, "Площадка должна иметь как минимум одну локацию");

            return locations;
        }

        private (string platformName, string[] locations) SplitLineIntoPlatformNameAndLocations(string line)
        {
            var parts = line.Split(':', 2, StringSplitOptions.TrimEntries);

            if (parts.Length != 2)
                throw new InvalidLineFormatException(_currentLine, "Формат строки: <название площадки>:<список локаций через,>");

            var locations = SplitLocationsLineIntoLocations(parts[1]);

            return (parts[0], locations);
        }

        private string[] SplitFileIntoLines(string rawFile)
        {
            return rawFile.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        }

        public Task<LocationNode> ParseFileToLocationsTree(string rawFile)
        {
            var root = new LocationNode()
            {
                Location = "root"
            };

            foreach (var line in SplitFileIntoLines(rawFile))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                _currentLine = line;

                (var platformName, var locations) = SplitLineIntoPlatformNameAndLocations(line);

                foreach (var location in locations)
                {
                    var locationAreas = SplitLocationIntoAreas(location);

                    var currentNode = root;

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

            return Task.FromResult(root);
        }

        public Task<List<string>> ParseLocationToAreas(string location)
        {
            _currentLine = location;
            
            return Task.FromResult(SplitLocationIntoAreas(location).ToList());
        }
    }
}
