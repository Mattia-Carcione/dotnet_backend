namespace Interfaces
{
    /// <summary>
    /// Defines a contract the exposes method to create services.
    /// </summary>
    /// <typeparam name="T">The type of the interface.</typeparam>
    public interface IFactoryService<T> where T : class
    {
        /// <summary>
        /// Creates a service.
        /// </summary>
        /// <returns>
        /// A task representing asynchronous operation that returns a new instance of <typeparamref name="T"/>.
        /// </returns>
        Task<T> CreateService();
    }
}
