using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;


namespace WebDriverGrabber
{
    public class WebGrabber : IGrabber
    {
        public void Download(string targetUrl, string targetPath)
        {
            var webClient = new WebClient();
            webClient.DownloadFile(targetUrl, targetPath);
        }

        public string Get(string targetUrl)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("WebDriverGrabber", "0.1")); 
            return httpClient.GetStringAsync(targetUrl).Result;
        }
    }
}
