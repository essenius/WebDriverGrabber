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

namespace WebDriverGrabber
{
    public class Browser
    {
        /// <summary>name of the browser. Is used to create a file in the target folder with the currently downloaded version</summary>
        public string Name { get; set; }

        /// <summary>Driver version to download. Use either this or VersionUrl</summary>
        public string Version { get; set; }
        
        /// <summary>URL of the currently stable driver version. Use either this or Version</summary>
        public string VersionUrl { get; set; }

        /// <summary>
        /// Optional. if the VersionUrl contains more than just a version number, this is the regex expression to extract the version.
        /// An example is \\\"tag_name\\\":\\\"(v[\\d\\.]+)\\\"  which extracts the version number at the first encounter of "tag_name":"vx.y.z".
        /// Note the use of the backslash to escape backslashes and quotes, which is necessary in Json.
        /// </summary>
        public string VersionExtractionRegex { get; set; }
        
        /// <summary>The URL template for the driver download. Parameter {0} is the version</summary>
        public string DriverUrlTemplate { get; set; }
    }
}
