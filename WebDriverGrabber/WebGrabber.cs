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

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;


namespace WebDriverGrabber
{
    public class WebGrabber : IGrabber
    {
        public void Download(string targetUrl, string targetPath)
        {
            var webClient = new WebClient();
            webClient.DownloadFile(targetUrl, targetPath);
        }

        public string Get(string targetUrl)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("WebDriverGrabber", "0.1")); 
            return httpClient.GetStringAsync(targetUrl).Result;
        }
    }
}
