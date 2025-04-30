using DownloadFiles.Library.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class BlobController : ControllerBase
{
    private readonly IBlobService _blobService;

    public BlobController(IBlobService blobService)
    {
        _blobService = blobService;
    }

    [HttpGet("download/{blobName}")]
    public async Task<IActionResult> DownloadBlob(string blobName)
    {
        try
        {
            Stream blobStream = await _blobService.DownloadBlobAsync(blobName);
            return File(blobStream, "application/octet-stream", blobName);
        }
        catch (FileNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }
}
