using PuppeteerSharp.Models;
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
                Path = "C:\\Chromium" //You can replace with custom path. 
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

        public static async Task<PuppeteerResponse> ConvertHtmlToPdf()
        {
            var response = new PuppeteerResponse();
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

                response.Message = "Pdf Created Succesfully";
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Message = "Error Occured! Detail: " + ex.Message;
                response.IsSuccess = false;
            }
            finally
            {
                CloseChromium(page);
            }
            return response;
        }

        public static async Task<PuppeteerResponse> TakeScreenShot()
        {
            var response = new PuppeteerResponse();
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

                response.Message = "Screenshot has taken succesfully";
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Message = "Error Occured! Detail: " + ex.Message;
                response.IsSuccess = false;
            }
            finally
            {
                CloseChromium(page);
            }
            return response;
        }

        public static async Task<string> TakeHtmlContent()
        {
            var page = await OpenChromiumPage();

            WaitUntilNavigation[] waitUntil = new[] { WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2, WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Load };
            await page.GoToAsync("http://www.google.com", new NavigationOptions { WaitUntil = waitUntil });

            return await page.GetContentAsync();
        }

        public static async Task<string> TakeHtmlContent(Page page)
        {
            return await page.GetContentAsync();
        }

        public static async Task<string> SetHtmlToPage()
        {
            var page = await OpenChromiumPage();

            WaitUntilNavigation[] waitUntil = new[] { WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2, WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Load };
            await page.SetContentAsync("<div><h1>Hello World!</h1></div>");

            return await page.GetContentAsync();
        }

        public static async Task<string> GetTitleOfPage()
        {
            var page = await OpenChromiumPage();

            WaitUntilNavigation[] waitUntil = new[] { WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2, WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Load };
            await page.GoToAsync("https://www.google.com", new NavigationOptions { WaitUntil = waitUntil });

            return await page.GetTitleAsync();
        }

        public static async Task<string> GetTitleOfPage(Page page)
        {
            return await page.GetTitleAsync();
        }

        public static async Task<Frame[]> GetAllFrames()
        {
            var page = await OpenChromiumPage();

            WaitUntilNavigation[] waitUntil = new[] { WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2, WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Load };
            await page.GoToAsync("https://www.google.com", new NavigationOptions { WaitUntil = waitUntil });
            return page.Frames;
        }

        public static async Task<PuppeteerResponse> LoginFacebook()
        {
            var response = new PuppeteerResponse();
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

                response.Message = "Pdf Created Succesfully";
                response.IsSuccess = true;

                await page.PdfAsync(Path.Combine(filePath, fileName));
            }

            catch (Exception ex)
            {
                response.Message = "Error Occured! Detail: " + ex.Message;
                response.IsSuccess = false;
            }
            finally
            {
                CloseChromium(page);
            }
            return response;
        }
    }
}
