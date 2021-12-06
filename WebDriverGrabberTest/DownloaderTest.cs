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
using System;
using System.IO;
using System.Reflection;
using WebDriverGrabber;

namespace WebDriverGrabberTest
{
    [TestClass]
    public class DownloaderTest
    {
        private object GetPrivateProperty(object target, string property) => 
            target.GetType()
                .GetProperty(property, BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(target, BindingFlags.DoNotWrapExceptions, null, null, null);

        private object InvokePrivateMethod(object target, string method) =>
            target.GetType()
                .GetMethod(method, BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(target, BindingFlags.DoNotWrapExceptions, null, null, null);

        [TestMethod]
        public void DownloaderLatestVersionStaticOkTest()
        {
            var browserConfig = new Browser
            {
                Version = "1.0.0.0"
            };
            var downloader = new Downloader(browserConfig, "", new MockGrabber());
            Assert.AreEqual("1.0.0.0", GetPrivateProperty(downloader, "LatestVersion"), "Latest version OK");
        }

        [TestMethod]
        public void DownloaderLatestVersionGrabOkTest()
        {
            var browserConfig = new Browser
            {
                VersionUrl = "https://irrelevant_as_mocked"
            };
            var downloader = new Downloader(browserConfig, "", new MockGrabber());
            Assert.AreEqual("2.0.0.2", GetPrivateProperty(downloader, "LatestVersion"), "Latest version OK");
        }


        [TestMethod]
        public void DownloaderLatestVersionRegexOkTest()
        {
            var browserConfig = new Browser
            {
                VersionUrl = "https://with_regex",
                VersionExtractionRegex = "\\\"version\\\":\\\"([\\d\\.]+)\\\""
            };
            var downloader = new Downloader(browserConfig, "", new MockGrabber());
            Assert.AreEqual("3.0.3.0", GetPrivateProperty(downloader, "LatestVersion"), "Latest version OK");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void DownloaderLatestVersionRegexBadTest()
        {
            var browserConfig = new Browser
            {
                VersionUrl = "https://with_regex",
                VersionExtractionRegex = "\\\"not_present\\\":\\\"([\\d\\.]+)\\\""
            };
            var downloader = new Downloader(browserConfig, "", new MockGrabber());
            _ = GetPrivateProperty(downloader, "LatestVersion");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void DownloaderLatestVersionNotFoundTest()
        {
            var downloader = new Downloader(new Browser(), "", new MockGrabber());
            _ = GetPrivateProperty(downloader, "LatestVersion");
        }

        [TestMethod]
        public void DownloaderLatestDownloadedFileTest()
        {
            var browserConfig = new Browser
            {
                Name = "Firefox"
            };
            var downloader = new Downloader(browserConfig, "H:\\", new MockGrabber());
            Assert.AreEqual("H:\\Firefox_latest.txt", GetPrivateProperty(downloader, "LatestDownloadedVersionFile"), "Latest downloaded file OK");
        }

        [TestMethod]
        public void DownloaderNeedsDownloadTest()
        {
            var browserConfig = new Browser
            {
                Name = "Edge",
            };
            var downloader = new Downloader(browserConfig, Path.GetTempPath(), new MockGrabber());
            Assert.IsTrue((bool)InvokePrivateMethod(downloader, "NeedsDownload"), "Needs Download");
        }

        [TestMethod]
        public void DownloaderNeedsDownloadExistingDriverTest()
        {
            var tempFile = Path.GetTempFileName();
            var browserName = Path.GetFileNameWithoutExtension(tempFile);
            var name = Path.GetFileNameWithoutExtension(tempFile) + "_latest.txt";
            var folder = Path.GetDirectoryName(tempFile);
            var versionFile = Path.Combine(folder, name);
            File.WriteAllText(versionFile, "1.0");
            var browserConfig = new Browser
            {
                Name = Path.GetFileNameWithoutExtension(tempFile),
                Version = "1.0",
                DriverUrlTemplate = versionFile
            };
            var downloader = new Downloader(browserConfig, folder, new MockGrabber());
            Assert.IsFalse((bool)InvokePrivateMethod(downloader, "NeedsDownload"), "Does not need download");
            File.Delete(versionFile);
        }

        [TestMethod]
        public void DownloaderDownloadTest()
        {
            var browserConfig = new Browser
            {
                Name = "Chrome",
                Version = "1.0",
                DriverUrlTemplate = @"http://localhost/test.txt"
            };
            var versionFile = Path.Combine(Path.GetTempPath(), browserConfig.Name + "_latest.txt");
            var driverFile = Path.Combine(Path.GetTempPath(), "test.txt");
            File.Delete(versionFile);
            File.Delete(driverFile);
            var downloader = new Downloader(browserConfig, Path.GetTempPath(), new MockGrabber());
            InvokePrivateMethod(downloader, "Download");
            Assert.IsTrue(File.Exists(versionFile), "Version file exists after");
            Assert.IsTrue(File.Exists(driverFile), "Driver file exists after");
            File.Delete(versionFile);
            File.Delete(driverFile);
        }
    }
}
