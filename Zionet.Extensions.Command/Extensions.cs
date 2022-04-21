using Microsoft.Extensions.DependencyInjection;

namespace Zionet.Extensions.Command
{
    /// <summary>
    /// Enable an easy <see cref="CommandManager"/> registration
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Register the <see cref="CommandManager"/>
        /// </summary>
        /// <param name="services">the service collection</param>
        /// <returns>the service collection</returns>
        public static IServiceCollection ConfigureCommander(this IServiceCollection services)
        {
            services.AddSingleton<ICommandManager, CommandManager>();
            return services;
        }
    }
}