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
