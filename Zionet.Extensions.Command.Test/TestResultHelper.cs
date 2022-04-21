using System.Collections.Generic;

namespace Zionet.Extensions.Command.Test
{
    public class TestResultHelper : ITestResultHelper
    {
        private readonly IList<string> _executionResults = new List<string>();

        public void AddInformation(string info)
        {
            _executionResults.Add(info);
        }

        public bool IsInformationExist(string info)
        {
            return _executionResults.Contains(info);
        }
    }
}