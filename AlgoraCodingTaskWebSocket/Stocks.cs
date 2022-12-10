using Newtonsoft.Json;
using System.Text;

public class Stocks : List<RandomStock>
{
    public readonly static Stocks Instance = new Stocks();

    public Stocks()
    {
        Add(new RandomStock { Name = "Stock1", Price = 240, Range = new Tuple<int, int>(240, 270) });
        Add(new RandomStock { Name = "Stock2", Price = 180, Range = new Tuple<int, int>(180, 210) });
    }

    public virtual void UpdatePrices()
    {
        ForEach(x => x.UpdatePrice());
    }

    public override string ToString()
    {
        var pair = new KeyValuePair<long, Stock[]>(DateTime.Now.Ticks, ToArray());
        var retval = JsonConvert.SerializeObject(pair);
        return retval;
    }

    public virtual byte[] ToBytes()
    {
        return Encoding.UTF8.GetBytes(ToString());
    }
}