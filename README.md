# WebDriverGrabber
Download browser drivers for different browsers to a folder of your choosing. It will also attempt to unzip the file into the same folder.

Command line parameter can be the path to a JSON confguration file. If not specified, it will search for WebDriverGrabber.json in the current directory
For an example see [WebDriverGrabber.json](WebDriverGrabber/WebDriverGrabber.json).

`targetFolder` is the folder where the browser drivers need to end up.

For each browser you want to support, the following parameters can be used:

* `name` is used to create a file containing the downloaded version tag, so future runs will not download versions unnecessarily.

* `versionUrl` is the Url to the version that you want to have. In the examples, it's the latest release.
If you want a specific verion, use `version` instead.

* If the versionUrl payload provides more than just the version, you can use a regex expression to extract the right part (`versionExtractionRegex`).

* `driverUrlTemplate` contains the URL template for the driver download. The parameter {0} is replaced by the version.
