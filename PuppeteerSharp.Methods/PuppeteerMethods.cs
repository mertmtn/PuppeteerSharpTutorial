using PuppeteerSharp.Models.Results;
using PuppeteerSharp.Models.Results.Error;
using PuppeteerSharp.Models.Results.Success;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PuppeteerSharp.Methods
{
    public static class PuppeteerMethods
    {
        public static async Task<Page> OpenChromiumPage()
        { 
            var browserFetcher = new BrowserFetcher(new BrowserFetcherOptions
            {
                Path = "D:\\Chromium" //You can replace with custom path. 
            }); 

            var revision = await browserFetcher.DownloadAsync(BrowserFetcher.DefaultRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Timeout = 0,
                ExecutablePath = revision.ExecutablePath 
            });
            return await browser.NewPageAsync();
        }

        public static async void CloseChromium(Page page)
        {
            if (!page.IsClosed && page != null)
            {                
                await page.Browser.CloseAsync();                
            } 
        }

        public static async Task<PuppeteerResult> ConvertHtmlToPdf()
        {             
            var page = await OpenChromiumPage();
            try
            {
                WaitUntilNavigation[] waitUntil = new[] { WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2, WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Load };
                await page.GoToAsync("http://www.google.com", new NavigationOptions { WaitUntil = waitUntil });

                var filePath = "D:\\PdfFiles";
                var fileName = Guid.NewGuid() + ".pdf";

                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                await page.PdfAsync(Path.Combine(filePath, fileName));
                return new SuccessPuppeteerResult("Pdf Created Succesfully");                 
            }
            catch (Exception ex)
            {
                return new ErrorPuppeteerResult("Error Occured! Detail: " + ex.Message);                
            }
            finally
            {
                CloseChromium(page);
            } 
        }

        public static async Task<PuppeteerResult> TakeScreenShot()
        {           
            var page = await OpenChromiumPage();
            try
            {
                WaitUntilNavigation[] waitUntil = new[] { WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2, WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Load };
                await page.GoToAsync("http://www.facebook.com", new NavigationOptions { WaitUntil = waitUntil });

                var filePath = "D:\\Screenshots";
                var fileName = Guid.NewGuid() + ".png";

                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                await page.SetViewportAsync(new ViewPortOptions
                {
                    Width = 1280,
                    Height = 720
                });

                await page.ScreenshotAsync(Path.Combine(filePath, fileName));
 
                return new SuccessPuppeteerResult("Screenshot has taken succesfully");
            }
            catch (Exception ex)
            {
                return new ErrorPuppeteerResult("Error Occured! Detail: " + ex.Message);
            }
            finally
            {
                CloseChromium(page);
            } 
        }

        public static async Task<string> TakeHtmlContent()
        {
            var page = await OpenChromiumPage();

            WaitUntilNavigation[] waitUntil = new[] { WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2, WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Load };
            await page.GoToAsync("http://www.google.com", new NavigationOptions { WaitUntil = waitUntil });

            return await page.GetContentAsync();
        }

        public static async Task<string> SetHtmlToPage()
        {
            var page = await OpenChromiumPage();

            WaitUntilNavigation[] waitUntil = new[] { WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2, WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Load };
            await page.SetContentAsync("<div><h1>Hello World!</h1></div>");

            return await page.GetContentAsync();
        }

        public static async Task<PuppeteerDataResult<string>> GetTitleOfPage()
        {
            var page = await OpenChromiumPage();
             
            try
            {
                WaitUntilNavigation[] waitUntil = new[] { WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2, WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Load };
                await page.GoToAsync("https://www.google.com", new NavigationOptions { WaitUntil = waitUntil });
                var title= await page.GetTitleAsync();
                return new SuccessPuppeteerDataResult<string>(title,"Get Title Successfully");
            }
            catch (Exception ex)
            {
                return new ErrorPuppeteerDataResult<string>("Error Occured! Detail: " + ex.Message);
            }
            finally
            {
                CloseChromium(page);
            }
        }               

        public static async Task<Frame[]> GetAllFrames()
        {
            var page = await OpenChromiumPage();

            WaitUntilNavigation[] waitUntil = new[] { WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2, WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Load };
            await page.GoToAsync("https://www.google.com", new NavigationOptions { WaitUntil = waitUntil });
            return page.Frames;
        }

        public static async Task<PuppeteerResult> LoginFacebook()
        {             
            var page = await OpenChromiumPage();

            try
            {
                WaitUntilNavigation[] waitUntil = new[] { WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2, WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Load };
                await page.GoToAsync("https://tr-tr.facebook.com/", new NavigationOptions { WaitUntil = waitUntil });


                await page.WaitForSelectorAsync("input#email");

                //You must change your facebook login informations.
                await page.TypeAsync("input#email", "Your Mail"); 
                await page.TypeAsync("input#pass", "Your Password");
                await page.ClickAsync("button[name='login']");
                await page.WaitForNavigationAsync();

                var HtmlContent = await page.GetContentAsync();
                var Cookie = await page.GetCookiesAsync();
                var TitleOfPage = await page.GetTitleAsync();

                var filePath = "D:\\PdfFiles";
                var fileName = Guid.NewGuid() + ".pdf";

                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                return new SuccessPuppeteerResult("Pdf Created Succesfully");
            }
            catch (Exception ex)
            {
                return new ErrorPuppeteerResult("Error Occured! Detail: " + ex.Message);
            } 
            finally
            {
                CloseChromium(page);
            }
           
        }
    }
}
