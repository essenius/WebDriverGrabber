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
using System.IO;
using WebDriverGrabber;

namespace WebDriverGrabberTest
{
    [TestClass]
    public class ConfigurationTest
    {
        [TestMethod]
        public void CreateConfgurationOkTest()
        {
            var config = Configuration.CreateConfiguration(null);
            Assert.AreEqual("%localappdata%\\BrowserDrivers", config.TargetFolder, "TargetFolder filled");
            Assert.IsTrue(config.ExpandedTargetFolder.Contains("AppData\\Local\\BrowserDrivers"), "ExpandedTargetFolder expanded");
            Assert.IsTrue(config.Browsers.Count > 0, "Browser config found");
            var browser = config.Browsers[0];
            Assert.AreEqual("ChromeDriver", browser.Name, "Name OK");
            Assert.IsNull(browser.Version, "Version is null");
            Assert.IsTrue(browser.VersionUrl.Contains("https://"), "VersionUrl contains an URL");
            Assert.IsNull(browser.VersionExtractionRegex, "VersionExtractionRegex is null");
            Assert.IsTrue(browser.DriverUrlTemplate.Contains("https://"), "DriverUrlTemplate contains an URL");

        }


        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void CreateConfgurationNotFoundTest()
        {
            _ = Configuration.CreateConfiguration("Missing.json");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]

        public void CreateConfgurationWrongContentTest()
        {
            _ = Configuration.CreateConfiguration("WrongContent.json");
        }
    }
}
