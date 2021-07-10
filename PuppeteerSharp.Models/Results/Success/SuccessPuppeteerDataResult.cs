 
namespace PuppeteerSharp.Models.Results.Success
{
    public class SuccessPuppeteerDataResult<T> : PuppeteerDataResult<T>
    {
        public SuccessPuppeteerDataResult(T data, string message) : base(data, true, message)
        {

        }

        public SuccessPuppeteerDataResult(T data) : base(data, true)
        {

        }

        public SuccessPuppeteerDataResult(string message) : base(default, true, message)
        {

        }

        public SuccessPuppeteerDataResult() : base(default, true)
        {

        }
    }
}
