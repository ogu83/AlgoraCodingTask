var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(1)
};
app.UseWebSockets(webSocketOptions);

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/ticker")
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            await webSocket.SendTicker();
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
    else
    {
        await next(context);
    }
});

app.MapGet("/", () => "This is an Web Socket Protocol for Stock Prices please follow ws://localhost:5000/ticker");
app.Run();