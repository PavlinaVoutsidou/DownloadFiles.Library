using Azure.Storage.Blobs;
using Azure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;

namespace DownloadFiles.Library.Helper
{
    public static class Helper 
    {
        public static BlobServiceClient GetBlobServiceClient(string accountName, string accountKey)
        {
            Azure.Storage.StorageSharedKeyCredential sharedKeyCredential =
                new StorageSharedKeyCredential(accountName, accountKey);

            string blobUri = $"https://{accountName}.blob.core.windows.net";

            BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(blobUri), sharedKeyCredential);
            return blobServiceClient; 
        }
    }
}
