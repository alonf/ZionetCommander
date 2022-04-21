using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Zionet.Extensions.Command
{
    /// <summary>
    /// The command dispatcher implementation
    /// </summary>

    class CommandManager : ICommandManager
    {
        private Dictionary<string, (MethodInfo MethodInfo, object? Target)> _commands = new Dictionary<string, (MethodInfo, object?)>();
        private readonly ILogger<CommandManager> _logger;

        /// <summary>
        /// Handle command discovery
        /// </summary>
        /// <param name="serviceProvider">The source for command implementation services</param>
        /// <param name="logger"></param>
        public CommandManager(IServiceProvider serviceProvider, ILogger<CommandManager> logger)
        {
            var commands = (from service in serviceProvider.GetServices<ICommandHandler>()
                            from method in service.GetType().GetMethods(BindingFlags.Default|BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Static)
                            let attribute = method.GetCustomAttribute<CommandAttribute>()
                            where attribute != null
                            select new { Target = service, Method = method, Message = attribute.Message }).ToArray();

            foreach (var command in commands)
            {
                _commands.Add(command.Message, (command.Method, command.Target));
            }
            _logger = logger;
            _logger.LogInformation($"Found {commands.Length} command handlers");
        }


        public void Register(string message, Action<string> action)
        {
            _commands.Add(message, (action.Method, action.Target));
        }

        public T? ExecuteInternal<T>(params object[] parameters) where T : class
        {
            string message = (string)parameters[0];

            if (_commands.TryGetValue(message, out var command))
            {
                _logger.LogInformation($"Executing command {command.Target?.GetType().Name}.{command.MethodInfo.Name} with message {message}");
                return command.MethodInfo.Invoke(command.Target, parameters) as T;
            }
            else
            {
                throw new KeyNotFoundException(message);
            }
        }

        public void Execute<T>(string message, T? data = default(T))
        {
            ExecuteInternal<object>(message, data!);
        }

        public void Execute(string message)
        {
            ExecuteInternal<object>(message);
        }

        public async Task ExecuteAsync<T>(string message, T? data = default(T))
        {
            await ExecuteInternal<Task>(message, data!)!;
        }

        public async Task ExecuteAsync(string message)
        {
            await ExecuteInternal<Task>(message)!;
        }
    }
}