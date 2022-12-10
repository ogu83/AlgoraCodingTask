using Newtonsoft.Json;

public abstract class Stock
{
    [JsonProperty]
    public required string Name { get; set; }
    [JsonProperty]
    public required float Price { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}