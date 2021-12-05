namespace WebDriverGrabber
{
    public interface IGrabber
    {
        public void Download(string targetUrl, string targetPath);
        public string Get(string targetUrl);
    }
}
