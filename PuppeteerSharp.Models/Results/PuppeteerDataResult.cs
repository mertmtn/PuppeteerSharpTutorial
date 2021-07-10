
namespace PuppeteerSharp.Models.Results
{
    public class PuppeteerDataResult<T> : PuppeteerResult, IDataResult<T>
    {
        public PuppeteerDataResult(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }

        public PuppeteerDataResult(T data, bool success) : base(success)
        {
            Data = data;
        }

        public T Data { get; }


    }
}
