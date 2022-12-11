using System.Linq;
using System.Windows;
using System.Windows.Navigation;

namespace AlgoraCodingTaskClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        const string defTickerUrl = "ws://localhost:5000/ticker";
        public IPriceService priceClient {  get; protected set; } = new WsClient();
        public string SocketUrl { get; protected set; } = defTickerUrl;

        protected override void OnStartup(StartupEventArgs e)
        {
            var url = e.Args.FirstOrDefault(defTickerUrl);
            SocketUrl = url ?? defTickerUrl;
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            priceClient.Dispose();
            base.OnExit(e);
        }
    }
}