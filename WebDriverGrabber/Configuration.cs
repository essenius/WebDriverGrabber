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

using System.Collections.Generic;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebDriverGrabber
{
    public class Configuration
    {
        /// <summary>Create a Configuration object from a Json configuration file.</summary>
        /// <param name="configurationFilePath">The path to a Json configuration file.
        /// If no file path is given, it searches for WebDriverGrabber.json in the current directory</param>
        /// <returns>The configuration object</returns>
        public static Configuration CreateConfiguration(string configurationFilePath)
        {
            if (string.IsNullOrEmpty(configurationFilePath)) {
                configurationFilePath = "WebDriverGrabber.json";
            }
            if (!File.Exists(configurationFilePath))
            {
                throw new FileNotFoundException($"Config file {configurationFilePath} not found");
            }
            var configData = File.ReadAllText(configurationFilePath);
            return CreateConfigurationFromString(configData, configurationFilePath);
        }

        private static Configuration CreateConfigurationFromString(string configData, string source)
        {
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var config =  JsonSerializer.Deserialize<Configuration>(configData, options);
            config.Source = source;
            config.Validate();
            return config;
        }

        /// <summary>The folder where the drivers need to end up</summary>
        public string TargetFolder { get; set; }

        /// <summary>The list of browsers for which we need drivers</summary>
        public IList<Browser> Browsers { get; set; }

        /// <summary>The path to the configuration source file. Do not set in Json, will be overwritten</summary>
        [JsonIgnore]
        public string Source { get; private set; }

        [JsonIgnore]
        public string ExpandedTargetFolder => Environment.ExpandEnvironmentVariables(TargetFolder);

        public void Validate()
        {
            if (Browsers == null)
            {
                throw new InvalidDataException($"No browser configuration data found in {Source}");
            }
        }
    }
}

