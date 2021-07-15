using PuppeteerSharp.Methods; 
using System;
using System.Threading.Tasks;

namespace PuppeteerSharp.ConsoleApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await GetVideoUrlList("http://www.google.com", "Beşiktaş");
            await LoginFacebook();
            Console.ReadKey();
        }
        private static async Task GetVideoUrlList(string url, string searchWord)
        {
            var response = await PuppeteerMethods.GetVideoSearchVideoResultUrlList(url, searchWord);

            if (response.Success && response.Data != null)
            {
                Console.WriteLine("Video sayısı:" + response.Data.Count);
                foreach (var videoLink in response.Data)
                {
                    Console.WriteLine(videoLink);
                }
            }
            else
            {
                Console.WriteLine(response.Message);
            }
        }

        private static async Task LoginFacebook()
        {
            var response = await PuppeteerMethods.LoginFacebook("UserName", "Password");

            if (response.Success)
            {
                Console.WriteLine(response.Message);
            }
            else
            {
                Console.WriteLine(response.Message);
            }
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
