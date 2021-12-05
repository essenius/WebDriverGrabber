using System;
using System.IO;

namespace WebDriverGrabber
{

    /// <summary>Creates target folder if needed and then downloads all drivers that are needed</summary>
    public class MainHelper
    {
        private Configuration _config;
        private IGrabber _grabber;

        public MainHelper(Configuration config, IGrabber grabber)
        {
            _config = config;
            _grabber = grabber;
        }

        public void Run()
        {
            if (!string.IsNullOrEmpty(_config.TargetFolder))
            {
                Directory.CreateDirectory(_config.ExpandedTargetFolder);
            }

            foreach (var browser in _config.Browsers)
            {
                Console.WriteLine($"Browser: {browser.Name}");
                var downloader = new Downloader(browser, _config.ExpandedTargetFolder, _grabber);
                downloader.DownloadIfNeeded();
            }

        }
    }
}
