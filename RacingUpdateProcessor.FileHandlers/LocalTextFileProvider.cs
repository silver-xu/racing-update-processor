using RacingUpdateProcessor.FileHandlers.Interfaces;

namespace RacingUpdateProcessor.FileHandlers
{
    public class LocalTextFileProvider : ITextFileProvider
    {
        private const int bufferSize = 4096;

        public async Task<string> Download(string url)
        {
            await using var fileStream = new FileStream(url, FileMode.Open, FileAccess.Read, FileShare.None, bufferSize, true);
            using var streamReader = new StreamReader(fileStream);

            return await streamReader.ReadToEndAsync();
        }

        public async Task Upload(string url, string data)
        {
            var folderPath = Path.GetDirectoryName(url);
            if (!Directory.Exists(folderPath) && !(folderPath is null))
            {
                Directory.CreateDirectory(folderPath);
            }

            await using var fileStream = new FileStream(url, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, bufferSize, true);
            using var streamWriter = new StreamWriter(fileStream);
            await streamWriter.WriteAsync(data);
        }
    }
}
