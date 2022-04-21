namespace Zionet.Extensions.Command.Test
{
    public interface ITestResultHelper
    {
        void AddInformation(string info);
        bool IsInformationExist(string info);
    }
}