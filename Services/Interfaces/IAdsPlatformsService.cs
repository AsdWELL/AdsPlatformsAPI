namespace AdsPlatformsAPI.Services.Interfaces
{
    public interface IAdsPlatformsService
    {
        Task LoadLocations(IFormFile file);

        Task<List<string>> GetPlatformsByLocation(string location);
    }
}
