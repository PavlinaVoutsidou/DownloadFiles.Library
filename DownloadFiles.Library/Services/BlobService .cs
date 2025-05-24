using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DownloadFiles.Library.Helper;
using Polly;
using Polly.Retry;

namespace DownloadFiles.Library.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobContainerClient _containerClient;
        private readonly AsyncRetryPolicy _retryPolicy;

        public BlobService(string accountName, string accountKey, string containerName)
        {
            var blobServiceClient = Helper.Helper.GetBlobServiceClient(accountName, accountKey);
            _containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            _retryPolicy = Policy
                .Handle<RequestFailedException>()
                .Or<TaskCanceledException>()
                .WaitAndRetryAsync(3, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, delay, retryCount, context) =>
                    {
                        Console.WriteLine($"[Retry {retryCount}] Retrying in {delay.TotalSeconds}s due to: {exception.Message}");
                    });
        }

        public async Task<Stream> DownloadBlobAsync(string blobName)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);

            if (!await blobClient.ExistsAsync())
                throw new FileNotFoundException($"Blob '{blobName}' not found.");

            var downloadResponse = await _retryPolicy.ExecuteAsync(() =>
                blobClient.DownloadAsync());

            return downloadResponse.Value.Content;
        }
    }
}
