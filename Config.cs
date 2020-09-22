using System;
using System.IO;
using System.Text.Json;

namespace DataFellows.HttpConsumer
{
    public class Configuration
    {
        private static Configuration _configuration;
        public HttpConfiguration Web { get; set; }
        public AdlsConfiguration AzureStorage { get; set; }

        public static Configuration GetConfiguration(string[] args)
        {
            if (_configuration == null)
            {
                _configuration = new Configuration();
                _configuration.Web = new HttpConfiguration();
                _configuration.AzureStorage = new AdlsConfiguration();
            }


            string config = "";
            if (args.Length > 0 && args[0].ToUpperInvariant().EndsWith(".JSON"))
                config = args[0];
            else
            if (Environment.GetEnvironmentVariable("DF_CONFIG") != null)
                config = Environment.GetEnvironmentVariable("DF_CONFIG");

            Console.WriteLine("{0} {1}", Directory.GetCurrentDirectory(), "config.json");
            if (Directory.GetFiles(Directory.GetCurrentDirectory(), "config.json", new EnumerationOptions() {MatchCasing = MatchCasing.PlatformDefault}).Length > 0)
                config = Path.Join(Directory.GetCurrentDirectory(), "config.json");

            if (!string.IsNullOrEmpty(config))
                DeserializeConfigFile(config);

            ParseEnvironmentVariables();
            return _configuration;
        }

        private static void DeserializeConfigFile(string filename)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true
            };
            _configuration = JsonSerializer.Deserialize<Configuration>(File.ReadAllText(filename), options);
        }

        private static void ParseEnvironmentVariables()
        {
            if (Environment.GetEnvironmentVariable("DF_HTTP_URI") != null)
                _configuration.Web.Uri = Environment.GetEnvironmentVariable("DF_HTTP_URI");

            if (Environment.GetEnvironmentVariable("DF_STORAGE_CS") != null)
                _configuration.AzureStorage.ConnectionString = Environment.GetEnvironmentVariable("DF_STORAGE_CS");

            if (Environment.GetEnvironmentVariable("DF_CONTAINER") != null)
                _configuration.AzureStorage.Container = Environment.GetEnvironmentVariable("DF_CONTAINER"); 

            if (Environment.GetEnvironmentVariable("DF_FILENAME") != null)
                _configuration.AzureStorage.Filename = Environment.GetEnvironmentVariable("DF_FILENAME");                                         
        }

        public override string ToString()
        {
            return $"Calling web resource: {_configuration.Web.Uri}";
        }
    }
}