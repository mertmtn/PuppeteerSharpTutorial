using PuppeteerSharp.Models.Results;
using PuppeteerSharp.Models.Results.Error;
using PuppeteerSharp.Models.Results.Success;
using System;
using System.Collections.Generic;
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

            using (var page = await OpenChromiumPage())
            {
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
        }

        public static async Task<PuppeteerResult> TakeScreenShot(string url)
        {
            using (var page = await OpenChromiumPage())
            {
                try
                {
                    WaitUntilNavigation[] waitUntil = new[] { WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2, WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Load };
                    await page.GoToAsync(url, new NavigationOptions { WaitUntil = waitUntil });

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
        }

        public static async Task<string> TakeHtmlContent()
        {
            using (var page = await OpenChromiumPage())
            {
                WaitUntilNavigation[] waitUntil = new[] { WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2, WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Load };
                await page.GoToAsync("http://www.google.com", new NavigationOptions { WaitUntil = waitUntil });

                return await page.GetContentAsync();
            }
        }

        public static async Task<string> SetHtmlToPage()
        {
            using (var page = await OpenChromiumPage())
            {
                WaitUntilNavigation[] waitUntil = new[] { WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2, WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Load };
                await page.SetContentAsync("<div><h1>Hello World!</h1></div>");

                return await page.GetContentAsync();
            }
        }

        public static async Task<PuppeteerDataResult<string>> GetTitleOfPage(string url)
        {
            using (var page = await OpenChromiumPage())
            {
                try
                {
                    WaitUntilNavigation[] waitUntil = new[] { WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2, WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Load };
                    await page.GoToAsync(url, new NavigationOptions { WaitUntil = waitUntil });
                    var title = await page.GetTitleAsync();
                    return new SuccessPuppeteerDataResult<string>(title, "Get Title Successfully");
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
        }

        public static async Task<Frame[]> GetAllFrames()
        {
            using (var page = await OpenChromiumPage())
            {
                WaitUntilNavigation[] waitUntil = new[] { WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2, WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Load };
                await page.GoToAsync("https://www.google.com", new NavigationOptions { WaitUntil = waitUntil });
                return page.Frames;
            }
        }

        public static async Task<PuppeteerResult> LoginFacebook()
        {
            using (var page = await OpenChromiumPage())
            {
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
                
        public static async Task<List<string>> VideoLinkList(Page page)
        {
            WaitUntilNavigation[] waitUntil = new[] { WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2, WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Load };            

            var next = @"document.getElementById('pnnext')";
            var nexts = await page.EvaluateExpressionAsync<object>(next);

            var linkList = new List<string>();
            var isLastPage = (nexts == null);

            while ((nexts != null) || !isLastPage)
            {
                var jsSelectAllAnchors = @"Array.from(document.querySelectorAll('.g'))";
                var urls = await page.EvaluateExpressionAsync<object[]>(jsSelectAllAnchors);

                for (int i = 0; i < urls.Length; i++)
                {
                    var query = $"document.querySelectorAll('.g')[{i}].getElementsByTagName('a')[0].href";
                    linkList.Add(await page.EvaluateExpressionAsync<string>(query));
                }

                nexts = await page.EvaluateExpressionAsync<object>(next);
                if (nexts != null)
                {
                    var nextHref = @"document.getElementById('pnnext').href";
                    var nextHrefUrl = await page.EvaluateExpressionAsync<string>(nextHref);

                    isLastPage = (nexts == null) && !string.IsNullOrEmpty(nextHrefUrl);

                    await page.GoToAsync(nextHrefUrl, new NavigationOptions { WaitUntil = waitUntil });
                    nexts = await page.EvaluateExpressionAsync<object>(next);
                }
                else
                {
                    isLastPage = true;
                }
            }
            return linkList;
        } 

        public static async Task<PuppeteerDataResult<List<string>>> GetVideoSearchVideoResultUrlList(string url, string searchWord)
        {
            using (var page = await OpenChromiumPage())
            {
                WaitUntilNavigation[] waitUntil = new[] { WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2, WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Load };
                await page.GoToAsync(url, new NavigationOptions { WaitUntil = waitUntil });

                await SearchOnGoogle(searchWord, page);

                //Click Video Tab
                var videoTabElement = @"document.getElementsByClassName('hdtb-mitem')[1].getElementsByTagName('a')[0].href";
                var videoTabLink = await page.EvaluateExpressionAsync<string>(videoTabElement);                
                await page.GoToAsync(videoTabLink, new NavigationOptions { WaitUntil = waitUntil });
                                
                var result= await VideoLinkList(page);                                 

                return new SuccessPuppeteerDataResult<List<string>>(result, "Get Title Successfully");               
            }
        }

        public static async Task<PuppeteerDataResult<string>> GetSearchStaticticDetail(string url, string searchWord)
        {
            using (var page = await OpenChromiumPage())
            {
                WaitUntilNavigation[] waitUntil = new[] { WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2, WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Load };
                await page.GoToAsync(url, new NavigationOptions { WaitUntil = waitUntil });

                await SearchOnGoogle(searchWord, page);

                var jsExpression = @"document.querySelectorAll('#result-stats')[0].innerHTML";
                var result = await page.EvaluateExpressionAsync<string>(jsExpression);

                return new SuccessPuppeteerDataResult<string>(result, "");
            }
        }

        private static async Task SearchOnGoogle(string searchWord, Page page)
        {
            WaitUntilNavigation[] waitUntil = new[] { WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2, WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Load };

            //Search Text Input
            await page.WaitForSelectorAsync("input[name='q']");
            await page.TypeAsync("input[name='q']", searchWord);

            //Search on Google Button and Click Operation
            await page.WaitForSelectorAsync("input[name='btnK']");
            await page.EvaluateExpressionAsync("document.querySelector(\"input[name='btnK']\").click()");

            await page.WaitForNavigationAsync(new NavigationOptions { WaitUntil = waitUntil });
        }
    }
}
