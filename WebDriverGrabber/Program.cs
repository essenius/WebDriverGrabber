
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
