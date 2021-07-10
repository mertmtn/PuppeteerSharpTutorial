
namespace PuppeteerSharp.Models.Results.Success
{
    public class SuccessPuppeteerResult : PuppeteerResult
    {
        public SuccessPuppeteerResult(string message) : base(true, message)
        {

        }

        public SuccessPuppeteerResult() : base(true)
        {

        }
    }
}
