using Microsoft.Extensions.DependencyInjection;

namespace Zionet.Extensions.Command.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCommander().AddSingleton<ITestResultHelper, TestResultHelper>();
            services.AddSingleton<ICommandHandler, CommandHandler>();
            services.AddSingleton<ICommandHandler, CommandHandler2>();
        }
    }
}