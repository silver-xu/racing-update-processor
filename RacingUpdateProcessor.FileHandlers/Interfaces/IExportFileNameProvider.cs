namespace RacingUpdateProcessor.FileHandlers.Interfaces
{
    /// <summary>
    /// Interface to generate export filename based on import filename
    /// </summary>
    public interface IExportFileNameProvider
    {
        /// <summary>
        /// Generate export filename based on the import filename
        /// </summary>
        /// <param name="importFileName">the import filename</param>
        /// <returns>the export filename</returns>
        public string GetExportFileName(string importFileName);
    }
}
