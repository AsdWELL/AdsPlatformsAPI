namespace AdsPlatformsAPI.Repositories.Interfaces
{
    public interface IAdsLocationsRepository
    {
        Task LoadLocations(string rawFile);

        Task<IEnumerable<string>> GetPlatformsByLocation(string location);
    }
}