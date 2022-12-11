using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AlgoraCodingTaskClient
{
    public partial class MainWindow : Window
    {
        private MainWindowVM _vm = new MainWindowVM();

        public MainWindow()
        {
            InitializeComponent();
            var app = (Application.Current as App);
            if (app != null)
                app.priceClient.OnResponseRecieved += OnResponseRecieved;

            DataContext = _vm;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            btnConnect_Click(this, e);
        }

        private async void OnResponseRecieved(KeyValuePair<long, Stock[]> response)
        {
            await Dispatcher.InvokeAsync(new Action(() =>
            {
                if (response.Value != null)
                {
                    foreach (var stock in response.Value)
                    {
                        _vm.Stocks.InsertOrUpdate(stock, response.Key);
                    }
                }
            }));
        }

        private async void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            var app = (Application.Current as App);
            if (app != null)
            {
                await app.priceClient.ConnectAsync(app.SocketUrl);
                _vm.Connected = true;
            }
        }

        private async void btnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            var app = (Application.Current as App);
            if (app != null)
            {
                await app.priceClient.DisconnectAsync();
                _vm.Connected = false;
            }
        }

        private void gridPrices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid)
            {
                var grid = (DataGrid)sender;
                if (grid.SelectedItem is StockVm)
                {
                    var selectedItem = (StockVm)grid.SelectedItem;
                    var selectedStockName = selectedItem.Name;
                    var historyWindow = new HistoryWindow(_vm.Stocks, selectedStockName);
                    historyWindow.Show();
                }
            }
        }
    }
}