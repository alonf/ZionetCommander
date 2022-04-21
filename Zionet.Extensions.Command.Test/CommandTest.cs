using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Zionet.Extensions.Command.Test
{

    public class CommandTest
    {
        private readonly ICommandManager _commandManager;
        private readonly ITestResultHelper _testResultHelper;

        public CommandTest(ICommandManager commandManager, ITestResultHelper testResultHelper)
        {
            _commandManager = commandManager;
            _testResultHelper = testResultHelper;
        }

        [Fact]
        public void TestSimpleCommand()
        {
            _commandManager.Execute("CommandA");
            Assert.True(_testResultHelper.IsInformationExist("CommandA"), "CommandA was not called");
        }

        [Fact]
        public void TestCommandWithData()
        {
            _commandManager.Execute("CommandB", new CommandData { AdditionalData = "data"});

            Assert.True(_testResultHelper.IsInformationExist("CommandB, data"), "CommandB should be called with additional data: data");
        }

        [Fact]
        public void TestSimpleCommand2()
        {
            _commandManager.Execute("CommandC");
            Assert.True(_testResultHelper.IsInformationExist("CommandC"), "CommandC was not called");
        }

        [Fact]
        public async Task TestAsyncCommand()
        {
            await _commandManager.ExecuteAsync("AsyncCommand");
            Assert.True(_testResultHelper.IsInformationExist("AsyncCommand"), "AsyncCommand was not called");
        }

        [Fact]
        public void TestNonExistingCommand()
        {
            Assert.Throws<KeyNotFoundException>(() => _commandManager.Execute("CommandD"));
        }
    }
}