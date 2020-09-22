using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace DataFellows.HttpConsumer
{
    public class AzureBlobService
    {
        private readonly BlobContainerClient _blobContainerClient;

        public AzureBlobService(string connectionString, string container)
        {
            _blobContainerClient = new BlobContainerClient(connectionString, container);
            _blobContainerClient.CreateIfNotExists();
        }
        public async Task<string> UploadBlobAsync(string  file, Stream stream)
        {
            string filename = file;
            BlobClient blobClient = _blobContainerClient.GetBlobClient(filename);
            await blobClient.UploadAsync(stream);
            return filename;
        }
    }
}
