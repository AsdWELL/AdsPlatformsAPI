namespace AdsPlatformsAPI.Services
{
    public interface IAdsPlatformsService
    {
        Task LoadLocations(string rawFile);

        Task<IEnumerable<string>> GetPlatformsByLocation(string location);
    }
}
