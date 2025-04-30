using Azure.Storage.Blobs;
using DownloadFiles.Library.Models;

namespace DownloadFiles.Library.Services
{
    public interface IBlobService
    {
        public Task<Stream> DownloadBlobAsync(string blobname);
    }
}
