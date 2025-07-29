using AdsPlatformsAPI.Models;

namespace AdsPlatformsAPI.Repositories.Interfaces
{
    public interface IAdsLocationsRepository
    {
        Task LoadLocations(LocationNode locationTreeRoot);

        Task<List<string>> GetPlatformsByLocationAreas(IEnumerable<string> locationAreas);
    }
}