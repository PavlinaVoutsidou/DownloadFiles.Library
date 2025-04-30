namespace DownloadFiles.Library.Models
{
    public class AzureResult
    {
        public string? status { get; set; }
        public string? message { get; set; }
        public DateTime? timestamp { get { return DateTime.Now; } }
    }
}
