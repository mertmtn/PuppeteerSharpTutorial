namespace PuppeteerSharp.Models.Results
{
    public class PuppeteerResult:IResult
    {       
        public PuppeteerResult(bool success)
        {
            Success = success;
        }

        //this kendini kastediyor. Result sınıfı
        public PuppeteerResult(bool success, string message) : this(success)
        {
            Message = message;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}
