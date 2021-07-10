using PuppeteerSharp.Methods; 
using System;
using System.Threading.Tasks;

namespace PuppeteerSharp.ConsoleApp
{
    public class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            await GetTitleOfPage();
            Console.ReadKey();
        }

        private static async Task GetTitleOfPage()
        {
            var response = await PuppeteerMethods.GetTitleOfPage();

            if (response.Success && !string.IsNullOrEmpty(response.Data))
            {
                Console.WriteLine("Sayfa Başlığı:" + response.Data);
            }
            else
            {
                Console.WriteLine(response.Message);
            }
        }
    }
}
