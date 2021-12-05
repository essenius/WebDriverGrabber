using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using WebDriverGrabber;

namespace WebDriverGrabberTest
{
    [TestClass]
    public class MainHelperTest
    {
        [TestMethod]
        public void MainHelperRunTest()
        {
            var config = new Configuration();
            config.TargetFolder = @"%localappdata%\test123";
            var browser = new Browser
            {
                Version = "3.0",
                Name = "Test",
                DriverUrlTemplate = "http://localhost/test.zip"
            };
            var versionFile = Path.Combine(config.ExpandedTargetFolder, browser.Name + "_latest.txt");
            var driverFile = Path.Combine(config.ExpandedTargetFolder, "test.zip");
            config.Browsers = new List<Browser>();
            config.Browsers.Add(browser);
            var helper = new MainHelper(config, new MockGrabber());
            helper.Run();

            Assert.IsTrue(File.Exists(versionFile), "Version file exists after");
            Assert.IsTrue(File.Exists(driverFile), "Driver file exists after");
            File.Delete(versionFile);
            File.Delete(driverFile);
        }
    }
}
