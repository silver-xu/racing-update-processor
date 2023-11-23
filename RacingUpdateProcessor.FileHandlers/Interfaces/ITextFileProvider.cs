namespace RacingUpdateProcessor.FileHandlers.Interfaces;

/// <summary>
/// Interface to provide protocol independent file operations
///
/// For Example: Local File Storage, FTP, S3 etc
/// </summary>
public interface ITextFileProvider
{
    /// <summary>
    /// Download file asynchronously and return its
    /// contents in string
    /// </summary>
    /// <param name="url">Download Url</param>
    /// <returns>Content of the file in string</returns>
    public Task<string> Download(string url);

    /// <summary>
    /// Upload file asynchronously
    /// </summary>
    /// <param name="url">Upload Url</param>
    /// <param name="data">data to upload in string</param>
    /// <returns>void</returns>
    public Task Upload(string url, string data);
}
