using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace AlgoraCodingTaskClient
{
    public class WsClient : IPriceService
    {
        public readonly int ReceiveBufferSize = 8192;
        private ClientWebSocket WS;
        private CancellationTokenSource CTS;

        public event OnResponseRecievedEventHandler OnResponseRecieved;

        public async Task ConnectAsync(string url)
        {
            if (WS != null)
            {
                if (WS.State == WebSocketState.Open) return;
                else WS.Dispose();
            }

            WS = new ClientWebSocket();

            if (CTS != null)
                CTS.Dispose();

            CTS = new CancellationTokenSource();

            await WS.ConnectAsync(new Uri(url), CTS.Token);
            await Task.Factory.StartNew(ReceiveLoop, CTS.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            await WS.SendAsync(new ArraySegment<byte>("OK"u8.ToArray()), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task DisconnectAsync()
        {
            if (WS is null) return;
            // TODO: requests cleanup code, sub-protocol dependent.
            if (WS.State == WebSocketState.Open)
            {
                CTS.CancelAfter(TimeSpan.FromSeconds(2));
                await WS.CloseOutputAsync(WebSocketCloseStatus.Empty, "", CancellationToken.None);
                await WS.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
            }
            WS.Dispose();
            CTS.Dispose();
        }

        private async Task ReceiveLoop()
        {
            var loopToken = CTS.Token;
            MemoryStream outputStream = null;
            WebSocketReceiveResult receiveResult = null;
            var buffer = new byte[ReceiveBufferSize];
            try
            {
                while (!loopToken.IsCancellationRequested)
                {
                    outputStream = new MemoryStream(ReceiveBufferSize);
                    do
                    {
                        receiveResult = await WS.ReceiveAsync(buffer, CTS.Token);
                        if (receiveResult.MessageType != WebSocketMessageType.Close)
                            outputStream.Write(buffer, 0, receiveResult.Count);
                    }
                    while (!receiveResult.EndOfMessage);
                    if (receiveResult.MessageType == WebSocketMessageType.Close) break;
                    outputStream.Position = 0;
                    await ResponseReceived(outputStream);
                }
            }
            catch (TaskCanceledException) { }
            finally
            {
                outputStream?.Dispose();
            }
        }

        private async Task ResponseReceived(Stream inputStream)
        {
            using (StreamReader rd = new StreamReader(inputStream))
            {
                var response = await rd.ReadToEndAsync();
                Debug.WriteLine(response);

                if (OnResponseRecieved != null)
                {
                    var responseObject = JsonConvert.DeserializeObject<KeyValuePair<long, Stock[]>>(response);
                    OnResponseRecieved(responseObject);
                }

                await WS.SendAsync(new ArraySegment<byte>("OK"u8.ToArray()), WebSocketMessageType.Text, true, CancellationToken.None);

            }
        }

        public void Dispose() => DisconnectAsync().Wait();
    }
}
