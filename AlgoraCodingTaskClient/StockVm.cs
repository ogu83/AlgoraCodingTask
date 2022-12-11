using System.Windows.Media;

namespace AlgoraCodingTaskClient
{
    public class StockVm : BaseVm
    {
        public static StockVm FromStock(Stock stock)
        {
            return new StockVm { Name = stock.Name, Price = stock.Price };
        }

        public void UpdatePriceWithStock(Stock stock)
        {
            Price = stock.Price;
        }

        private float _price;
        public float Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (value != _price)
                {
                    if (value < _price)
                        PriceColor = Brushes.Red;
                    else if (value > _price)
                        PriceColor = Brushes.Green;
                    else
                        PriceColor = Brushes.White;
                    NotifyPropertyChanged(nameof(PriceColor));

                    _price = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(PriceStr));
                }
            }
        }

        private string _name = string.Empty;
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (value != _name)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string PriceStr { get { return _price.ToString("n2"); } }

        public SolidColorBrush PriceColor { get; private set; } = Brushes.White;
    }
}