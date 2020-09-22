using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataFellows.HttpConsumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Configuration configuration = Configuration.GetConfiguration(args);
            LogMessage(configuration.ToString());

            HttpClient client = new HttpClient();
            AzureBlobService blobService = new AzureBlobService(configuration.AzureStorage.ConnectionString, configuration.AzureStorage.Container);
            string output = await blobService.UploadBlobAsync(configuration.AzureStorage.Filename, await client.GetStreamAsync(configuration.Web.Uri));
            LogMessage($" File written to {output}.");
        }

        static void LogMessage(string message)
        {
            Console.WriteLine($"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")} {message}");
        }

        static void LogError(string message)
        {
            Console.Error.WriteLine($"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")} {message}");
        }
    }
}
