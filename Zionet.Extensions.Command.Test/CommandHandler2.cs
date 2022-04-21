using System.Threading.Tasks;
using Xunit.DependencyInjection;

namespace Zionet.Extensions.Command.Test
{
    public class CommandHandler2 : ICommandHandler
    {
        private readonly ITestResultHelper _testResultHelper;

        public CommandHandler2(ITestResultHelper testResultHelper)
        {
            _testResultHelper = testResultHelper;
        }

        [Command("CommandC")]
        public void OnCommandC(string message)
        {
            _testResultHelper.AddInformation(message);
        }

        [Command("AsyncCommand")]
        public async Task OnCommandAsync(string message)
        {
            await Task.Yield();
            _testResultHelper.AddInformation(message);
        }
    }
}