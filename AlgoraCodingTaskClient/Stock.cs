using Newtonsoft.Json;

namespace AlgoraCodingTaskClient
{
    public class Stock
    {
        [JsonProperty]
        public required string Name { get; set; }
        [JsonProperty]
        public required float Price { get; set; }
    }
}