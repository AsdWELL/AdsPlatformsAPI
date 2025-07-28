namespace AdsPlatformsAPI.Models
{
    public class LocationNode
    {
        public string Location { get; set; } = string.Empty;

        public Dictionary<string, LocationNode> Childrens { get; set; } = [];

        public HashSet<string> Platforms { get; set; } = [];
    }
}