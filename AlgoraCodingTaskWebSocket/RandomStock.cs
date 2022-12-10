using Newtonsoft.Json;

public class RandomStock : Stock
{
    private readonly Random rnd = new Random();

    [JsonIgnore]
    public required Tuple<int, int> Range { get; set; }

    public virtual void UpdatePrice()
    {
        Price = rnd.NextSingle() * (Range.Item2 - Range.Item1) + Range.Item1;
    }
}