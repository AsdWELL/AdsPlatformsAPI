using AdsPlatformsAPI.Models;

namespace AdsPlatformsAPI.Services.Interfaces
{
    public interface IParsingService
    {
        Task<LocationNode> ParseFileToLocationsTree(string rawFile);
        Task<List<string>> ParseLocationToAreas(string location);
    }
}