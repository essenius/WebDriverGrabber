using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

using WebDriverGrabber;

namespace WebDriverGrabberTest
{
    [TestClass]
    public class MainTest
    {
        [TestMethod]
        public void MainTest1()
        {
            Program.Main(new[] { "MainTest.json" });
            var versionFile = Path.Combine(Path.GetTempPath(), "MainTest_latest.txt");
            var driverFile = Path.Combine(Path.GetTempPath(), "README.md");
            Assert.IsTrue(File.Exists(versionFile), "Version file exists after");
            Assert.IsTrue(File.Exists(driverFile), "Driver file exists after");
            File.Delete(versionFile);
            File.Delete(driverFile);
        }

    }
}
