
namespace PuppeteerSharp.Models.Results.Error
{
    public class ErrorPuppeteerDataResult<T> : PuppeteerDataResult<T>
    {
        public ErrorPuppeteerDataResult(T data, string message) : base(data, false, message)
        {

        }

        public ErrorPuppeteerDataResult(T data) : base(data, false)
        {

        }

        public ErrorPuppeteerDataResult(string message) : base(default, false, message)
        {

        }

        public ErrorPuppeteerDataResult() : base(default, false)
        {

        }
    }
}
