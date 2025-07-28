using AdsPlatformsAPI.Repositories.Interfaces;

namespace AdsPlatformsAPI.Services
{
    public class AdsPlatformsService(IAdsLocationsRepository repository) : IAdsPlatformsService
    {
        public Task LoadLocations(string rawFile)
        {
            return repository.LoadLocations(rawFile);
        }

        public Task<IEnumerable<string>> GetPlatformsByLocation(string location)
        {
            return repository.GetPlatformsByLocation(location);
        }
    }
}
