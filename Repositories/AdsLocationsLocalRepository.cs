using AdsPlatformsAPI.Exceptions;
using AdsPlatformsAPI.Models;
using AdsPlatformsAPI.Repositories.Interfaces;

namespace AdsPlatformsAPI.Repositories
{
    public class AdsLocationsLocalRepository : IAdsLocationsRepository
    {
        private LocationNode? _root;

        private bool IsLocationTreeCreated()
        {
            return _root != null;
        }

        public Task LoadLocations(LocationNode locationTreeRoot)
        {
            _root = locationTreeRoot;

            return Task.CompletedTask;
        }

        public Task<List<string>> GetPlatformsByLocationAreas(IEnumerable<string> locationAreas)
        {
            if (!IsLocationTreeCreated())
                throw new LocationsFileNotUploadedException();

            var platforms = new HashSet<string>();
            
            var currentNode = _root!;

            foreach (var area in locationAreas)
            {
                if (!currentNode.Childrens.TryGetValue(area, out var children))
                    break;
                
                currentNode = children;

                foreach (var platform in currentNode.Platforms)
                    platforms.Add(platform);
            }

            return Task.FromResult(platforms.ToList());
        }
    }
}
