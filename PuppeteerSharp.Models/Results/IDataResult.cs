using System;
namespace PuppeteerSharp.Models.Results
{
    interface IDataResult<T> : IResult
    {
        T Data { get; }
    }
}
