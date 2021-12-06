// Copyright 2021 Rik Essenius
//
//   Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file 
//   except in compliance with the License. You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software distributed under the License 
//   is distributed on an "AS IS" BASIS WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and limitations under the License.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
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
