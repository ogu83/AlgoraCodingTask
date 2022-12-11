using System.Windows;

namespace AlgoraCodingTaskClient
{
    /// <summary>
    /// Interaction logic for HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        private StocksVm _stocksVm;
        private string _filterByStockName;
        private HistoryWindowVM _vm;

        public HistoryWindow() : this(new StocksVm(),"")
        {
        }

        public HistoryWindow(StocksVm stocksVm, string filterByStockName)
        {
            InitializeComponent();
            _stocksVm = stocksVm;
            _filterByStockName = filterByStockName;
            _vm = new HistoryWindowVM(filterByStockName);
            _vm.History = _stocksVm.History;
            DataContext = _vm;
        }
    }
}