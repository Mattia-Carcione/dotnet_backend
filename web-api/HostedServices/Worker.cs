using Microsoft.AspNetCore.SignalR.Client;

namespace HostedServices
{
    /// <summary>
    /// A instance of <see cref="Worker"/> representing a hosted server. Implements <see cref="IHostedService"/>.
    /// </summary>
    public class Worker : IHostedService
    {
        /// <summary>
        /// A instance of <see cref="ILogger{T}"/>.
        /// </summary>
        private readonly ILogger<Worker> _logger;

        /// <summary>
        /// A instance representing a connection to the hub.
        /// </summary>
        private HubConnection _connection;

        /// <summary>
        /// Initializes a new instance of <see cref="Worker"/>.
        /// </summary>
        /// <remarks>
        /// Configures <see cref="HubConnectionBuilder"/> with the specified url.
        /// </remarks>
        /// <param name="logger"></param>
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;

            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:6001/ChatHub")
                .WithAutomaticReconnect()
                .Build();

            _connection.On<string>("ReceiveMessage", SendMessage);
        }

        /// <summary>
        /// Starts the connection to the hub.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing asynchronous operation.
        /// </returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                try
                {
                    await _connection.StartAsync(cancellationToken);

                    break;
                }
                catch
                {
                    await Task.Delay(1000, cancellationToken);
                }
            }
        }

        /// <summary>
        /// Logs a specified message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <returns>
        /// A task representing the complete operation.
        /// </returns>
        public Task SendMessage(string message)
        {
            _logger.LogInformation(message);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Closes the connection to the hub.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing asynchronous operation.
        /// </returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _connection.DisposeAsync();
        }
    }
}
