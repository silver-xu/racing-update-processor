namespace RacingUpdateProcessor.FileHandlers.Interfaces
{
    /// <summary>
    /// Interface to export data to a target url
    /// It provides abstraction on file types for example JSON file
    /// </summary>
    /// <typeparam name="T">The type of data</typeparam>
    public interface IExportProvider<T>
    {
        /// <summary>
        /// Export data to a target url
        /// </summary>
        /// <param name="url">Target url</param>
        /// <param name="data">Data inside the file</param>
        /// <returns>async void</returns>
        public Task Export(string url, T data);
    }
}
