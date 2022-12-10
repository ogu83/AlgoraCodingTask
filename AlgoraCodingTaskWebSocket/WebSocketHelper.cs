using System.Net.WebSockets;

public static class WebSocketHelper
{
    public static async Task SendTicker(this WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        var receiveResult = await webSocket.ReceiveAsync(
            new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!receiveResult.CloseStatus.HasValue)
        {
            Thread.Sleep(1000);
            Stocks.Instance.UpdatePrices();
            await webSocket.SendAsync(
                new ArraySegment<byte>(Stocks.Instance.ToBytes()), 
                WebSocketMessageType.Text, 
                true, 
                CancellationToken.None);

            receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), 
                CancellationToken.None);
        }

        await webSocket.CloseAsync(
            receiveResult.CloseStatus.Value,
            receiveResult.CloseStatusDescription,
            CancellationToken.None);
    }
}