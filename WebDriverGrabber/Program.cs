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
    /// <summary>
    /// Download browser drivers using a Json configuration file.
    /// If a path is given as the command line parameter, that is expected to be the configuration file.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var configFile = (args.Length > 0) ? args[0] : null;
            var config = Configuration.CreateConfiguration(configFile);
            new MainHelper(config, new WebGrabber()).Run();
        }
    }
}
