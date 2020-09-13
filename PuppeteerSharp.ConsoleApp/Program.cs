using PuppeteerShap.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuppeteerSharp.ConsoleApp
{
    public class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            var response= await PuppeteerMethods.LoginFacebook();            
      
            if (response.IsSuccess)
            {
                Console.WriteLine(response.Message);
            }
            else
            {
                Console.WriteLine(response.Message);
            }
            Console.ReadKey();
        }
    }
}
