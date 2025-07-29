using AdsPlatformsAPI.Repositories.Interfaces;
using AdsPlatformsAPI.Services.Interfaces;

namespace AdsPlatformsAPI.Services
{
    public class AdsPlatformsService(
        IAdsLocationsRepository locationsRepository,
        IParsingService parsingService) : IAdsPlatformsService
    {
        public async Task LoadLocations(IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var content = await reader.ReadToEndAsync();

                var locationTreeRoot = await parsingService.ParseFileToLocationsTree(content);

                await locationsRepository.LoadLocations(locationTreeRoot);
            }
        }

        public async Task<List<string>> GetPlatformsByLocation(string location)
        {
            var locationAreas = await parsingService.ParseLocationToAreas(location);

            return await locationsRepository.GetPlatformsByLocationAreas(locationAreas);
        }
    }
}
