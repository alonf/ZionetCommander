namespace Zionet.Extensions.Command
{
    /// <summary>
    /// Manages the command registration and dispatching
    /// </summary>
    public interface ICommandManager
    {
        /// <summary>
        /// A manual method to dispatch commands
        /// Prefer using the <see cref="CommandAttribute"/>
        /// </summary>
        /// <param name="message">The command selector</param>
        /// <param name="action">the command target</param>
        void Register(string message, Action<string> action);

        /// <summary>
        /// Dispatch a message to command handler with additional data
        /// </summary>
        /// <typeparam name="T">The type of the additional message data</typeparam>
        /// <param name="message">The command selector</param>
        /// <param name="data">The additional data</param>
        void Execute<T>(string message, T? data);

        /// <summary>
        /// Dispatch a message to command handler
        /// </summary>
        /// <param name="message"></param>
        void Execute(string msmessageg);

        /// <summary>
        /// Dispatch an async message to command handler with additional data
        /// </summary>
        /// <typeparam name="T">The type of the additional message data</typeparam>
        /// <param name="message">The command selector</param>
        /// <param name="data">The additional data</param>
        Task ExecuteAsync<T>(string message, T? data);

        /// <summary>
        /// Dispatch an async message to command handler
        /// </summary>
        /// <param name="message"></param>
        Task ExecuteAsync(string message);
    }
}