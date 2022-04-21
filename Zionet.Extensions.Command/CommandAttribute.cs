namespace Zionet.Extensions.Command
{
    /// <summary>
    /// Mark a method as a command handler. 
    /// The containing class must implement the <see cref="ICommandHandler"/> market interface
    /// The containing class needs to be registered as a dependency injected service
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        /// <summary>
        /// Mark a method as a command handler
        /// </summary>
        /// <param name="message">The command selector</param>
        public CommandAttribute(string message)
        {
            Message = message;
        }
        public string Message { get; }
    }
}