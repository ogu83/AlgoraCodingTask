using System.Linq;

namespace AlgoraCodingTaskClient
{
    public class HistoryWindowVM : BaseVm
    {
        public HistoryWindowVM(string stockName)
        {
            _stockName = stockName;
        }

        private void History_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(FilteredHistory));
        }

        private string _stockName;
        public string StockName { get { return _stockName; } }
        public string Title { get { return $"History Of {StockName}"; } }

        private StockHistory _history = new StockHistory();
        public StockHistory History
        {
            get { return _history; }
            set
            {
                if (_history != value)
                {
                    if (_history != null)
                        _history.CollectionChanged -= History_CollectionChanged;

                    _history = value;
                    _history.CollectionChanged += History_CollectionChanged;
                }
            }
        }

        public IOrderedEnumerable<StockHistoryItem> FilteredHistory
        {
            get
            {
                return History.Where(x => x.Name == _stockName).OrderByDescending(x => x.TimeStamp);
            }
        }
    }
}