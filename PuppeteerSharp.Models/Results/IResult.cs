namespace PuppeteerSharp.Models.Results
{
    internal interface IResult
    {        
        bool Success { get; }
        string Message { get; }
    }
}