using System.Collections.ObjectModel;
using System.Linq;

namespace AlgoraCodingTaskClient
{
    public class StocksVm : BaseVm
    {
        public void InsertOrUpdate(Stock stock, long ticks)
        {
            if (stock != null)
            {
                if (!_items.Any(x => x.Name == stock.Name))
                {
                    Items.Add(StockVm.FromStock(stock));
                }
                else
                {
                    Items.Single(x => x.Name == stock.Name).UpdatePriceWithStock(stock);
                }

                History.Add(StockHistoryItem.FromStock(stock, ticks));
            }
        }

        private ObservableCollection<StockVm> _items = new ObservableCollection<StockVm>();
        public ObservableCollection<StockVm> Items
        {
            get { return _items; }
            set
            {
                if (value != _items)
                {
                    _items = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public StockHistory History { get; set; } = new StockHistory();
    }
}