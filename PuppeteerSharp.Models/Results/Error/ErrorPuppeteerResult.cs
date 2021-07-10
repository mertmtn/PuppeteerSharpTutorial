
namespace PuppeteerSharp.Models.Results.Error
{
    public class ErrorPuppeteerResult : PuppeteerResult
    {
        public ErrorPuppeteerResult(string message) : base(false, message)
        {

        }

        public ErrorPuppeteerResult() : base(false)
        {

        }
    }
}
