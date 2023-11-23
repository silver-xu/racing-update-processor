namespace RacingUpdateProcessor.FileHandlers.Interfaces
{
    /// <summary>
    /// Interface to import a file based on url and validate against a schema
    /// It provides abstraction on file types for example XML files with xsd validation
    /// </summary>
    /// <typeparam name="T">Type of imported data</typeparam>
    public interface IImportProvider<T>
    {
        /// <summary>
        /// Import and validate a file from url
        /// </summary>
        /// <param name="schemaPath">Path to the schema file</param>
        /// <param name="url">Source url</param>
        /// <returns>Imported data</returns>
        public Task<T> Import(string schemaPath, string url);
    }
}
