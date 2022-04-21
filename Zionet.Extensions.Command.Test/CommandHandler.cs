using Xunit.DependencyInjection;

namespace Zionet.Extensions.Command.Test
{
    public class CommandHandler : ICommandHandler
    {
        private readonly ITestResultHelper _testResultHelper;

        public CommandHandler(ITestResultHelper testResultHelper)
        {
            _testResultHelper = testResultHelper;
        }

        [Command("CommandA")]
        public void OnCommandA(string message)
        {
            _testResultHelper.AddInformation(message);
        }

        [Command("CommandB")]
        public void OnCommandB(string message, CommandData data)
        {
            _testResultHelper.AddInformation($"{message}, {data.AdditionalData}");
        }

    }
}