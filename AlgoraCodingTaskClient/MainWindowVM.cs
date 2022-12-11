namespace AlgoraCodingTaskClient
{
    public class MainWindowVM : BaseVm
    {
        private StocksVm _stocks = new StocksVm();
        public StocksVm Stocks
        {
            get
            {
                return _stocks;
            }

            set
            {
                if (value != _stocks)
                {
                    _stocks = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool _connected;
        public bool Connected
        {
            get
            {
                return _connected;
            }

            set
            {
                if (value != _connected)
                {
                    _connected = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(NotConnected));
                }
            }
        }

        public bool NotConnected { get { return !_connected; } }
    }
}