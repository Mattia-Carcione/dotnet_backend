using Microsoft.AspNetCore.SignalR;

namespace NotificationHub
{
    /// <summary>
    /// A instance of <see cref="ChatHub"/> representing the hub connected to the server.
    /// </summary>
    public class ChatHub : Hub
    {
        /// <summary>
        /// Sends a message to all clients connected to the server.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <returns>
        /// A task representing asynchronous operation.
        /// </returns>
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
