using PuppeteerSharp.Methods; 
using System;
using System.Threading.Tasks;

namespace PuppeteerSharp.ConsoleApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await GetSearchStaticticDetail();
            Console.ReadKey();
        }

        private static async Task GetSearchStaticticDetail()
        {
            var response = await PuppeteerMethods.GetSearchStaticticDetail("http://www.google.com", "Beşiktaş");

            if (response.Success && !string.IsNullOrEmpty(response.Data))
            {
                Console.WriteLine(response.Data);
            }
            else
            {
                Console.WriteLine(response.Message);
            }
        }

        private static async Task GetVideoUrlList()
        {
            var response = await PuppeteerMethods.GetVideoSearchVideoResultUrlList("http://www.google.com", "ateşini yolla bana");

            if (response.Success && response.Data != null)
            {
                Console.WriteLine("Video sayısı:" + response.Data.Count);
            }
            else
            {
                Console.WriteLine(response.Message);
            }
        }

        private static async Task GetTitleOfPage()
        {
            var response = await PuppeteerMethods.GetTitleOfPage("http://www.google.com");

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
