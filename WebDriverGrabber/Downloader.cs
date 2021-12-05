using System;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace WebDriverGrabber
{
    public class Downloader
    {
        private Browser _browser;
        private string _targetFolder;
        private IGrabber _grabber;
        private string _latestVersion;

        public Downloader(Browser browser, string targetfolder, IGrabber grabber)
        {
            _browser = browser;
            _targetFolder = targetfolder;
            _grabber = grabber;
        }

        /// <summary>
        /// Get the driver version to download. If Version is specified, use that. Else, find the version by downloading the version at VersionUrl.
        /// If the payload contains more than just a version number, use a regex expression with a group to extract the version number.
        /// ZipFile.ExtractToDirectory(@"c:\users\rik.essenius\nope.zip", @"c:\usersrik.essenius", false)
        /// </summary>
        private string LatestVersion
        {
            get
            {
                if (string.IsNullOrEmpty(_latestVersion))
                {
                    if (!string.IsNullOrEmpty(_browser.Version)) {
                        _latestVersion = _browser.Version;
                        return _latestVersion;
                    }
                    if (string.IsNullOrEmpty(_browser.VersionUrl))
                    {
                        throw new FormatException($"Could not identify version for {_browser.Name}. Neither Version nor VersionUrl specified.");
                    }
                    _latestVersion = _grabber.Get(_browser.VersionUrl).TrimEnd();
                    if (string.IsNullOrEmpty(_browser.VersionExtractionRegex)) return _latestVersion;
                    Console.WriteLine(_browser.VersionExtractionRegex);
                    var regex = new Regex(_browser.VersionExtractionRegex);
                    var match = regex.Match(_latestVersion);
                    if (!match.Success)
                    {
                        throw new FormatException($"Could not find version for {_browser.Name} in {_browser.VersionUrl}.");
                    }
                    _latestVersion = match.Groups[1].Value;
                    Console.WriteLine($"Latest Version: {_latestVersion}");
                }
                return _latestVersion;
            }
        }

        /// <summary> 
        /// Get the path to the file name that contains version of the latest downloaded driver
        /// </summary>
        private string LatestDownloadedVersionFile => Path.Combine(_targetFolder, _browser.Name + "_latest.txt");

        private string SavePath
        {
            get
            {
                var driverUrl = string.Format(_browser.DriverUrlTemplate, LatestVersion);
                var driverName = Path.GetFileName(driverUrl);
                Console.WriteLine(driverUrl);
                Console.WriteLine(driverName);
                return Path.Combine(_targetFolder, driverName);
            }
        }

        /// <summary>
        /// Check if the browser needs to be downloaded. Do that by comparing the target version with the currently available version.
        /// If they are the same, return false. Otherwise resturn true (including when the local version doesn't exist).
        /// </summary>
        /// <returns></returns>
        private bool NeedsDownload()
        {
            if (File.Exists(LatestDownloadedVersionFile))
            {
                var latestDownloadedVersion = File.ReadAllText(LatestDownloadedVersionFile);
                if (LatestVersion.Equals(latestDownloadedVersion))
                {
                    Console.WriteLine($"Latest version {latestDownloadedVersion} already downloaded");
                    return false;
                }
            }
            return true;
        }

            
        /// <summary>Download the browser driver. This is expected to be a zip file, so extract it as well</summary>
        private void Download()
        {
            var driverUrl = string.Format(_browser.DriverUrlTemplate, LatestVersion);
            var driverName = Path.GetFileName(driverUrl);
            Console.WriteLine(driverUrl);
            Console.WriteLine(driverName);
            var savePath = Path.Combine(_targetFolder, driverName);
            Console.WriteLine($"Downloading {driverUrl}");
            _grabber.Download(driverUrl, SavePath);
            try
            {
                ZipFile.ExtractToDirectory(savePath, _targetFolder, true);
            } catch (InvalidDataException)
            {
                Console.WriteLine($"{savePath} is not a Zip file");
            }
            File.WriteAllText(LatestDownloadedVersionFile, LatestVersion);
        }

        /// <summary>Check if the driver needs to be downloaded and if so do it.</summary>
        public void DownloadIfNeeded()
        {
            if (NeedsDownload())
            {
                Download();
            }
        }

    }
}
