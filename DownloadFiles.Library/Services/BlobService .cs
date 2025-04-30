using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DownloadFiles.Library.Models;
using DownloadFiles.Library.Helper;

namespace DownloadFiles.Library.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobContainerClient _containerClient;

        public BlobService(string accountName, string accountKey, string containerName)
        {
            var blobServiceClient = Helper.Helper.GetBlobServiceClient(accountName, accountKey);
            _containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        }

        public async Task<Stream> DownloadBlobAsync(string blobName)
        {
            BlobClient blobClient = _containerClient.GetBlobClient(blobName);

            if (await blobClient.ExistsAsync())
            {
                BlobDownloadInfo download = await blobClient.DownloadAsync();
                return download.Content;
            }

            throw new FileNotFoundException($"Blob '{blobName}' not found.");
        }
    }
}
