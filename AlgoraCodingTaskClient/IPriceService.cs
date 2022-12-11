using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlgoraCodingTaskClient
{
    public delegate void OnResponseRecievedEventHandler(KeyValuePair<long, Stock[]> response);

    public interface IPriceService : IDisposable
    {
        Task ConnectAsync(string url);
        Task DisconnectAsync();
        event OnResponseRecievedEventHandler OnResponseRecieved;
    }
}
