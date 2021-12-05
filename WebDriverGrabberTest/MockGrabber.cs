using System.IO;
using WebDriverGrabber;

namespace WebDriverGrabberTest
{
    class MockGrabber : IGrabber
    {
        public void Download(string targetUrl, string targetPath)
        {
            File.WriteAllText(targetPath, "test");
        }

        public string Get(string targetUrl)
        {
            if (targetUrl.Contains("regex")) return "{\"version\":\"3.0.3.0\"}";
            return "2.0.0.2";
        }
    }
}
