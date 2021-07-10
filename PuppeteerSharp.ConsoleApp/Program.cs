using PuppeteerSharp.Methods; 
using System; 

namespace PuppeteerSharp.ConsoleApp
{
    public class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            var response= await PuppeteerMethods.GetTitleOfPage();            
      
            if (response.Success && !string.IsNullOrEmpty(response.Data))
            {
                Console.WriteLine(response.Data);
            }
            else
            {
                Console.WriteLine(response.Message);
            }
            Console.ReadKey();
        }
    }
}
