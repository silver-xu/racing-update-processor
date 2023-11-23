using RacingUpdateProcessor.Models.Abstract;

namespace RacingUpdateProcessor.FileHandlers.Interfaces
{
    /// <summary>
    /// Wrapper interface to provide import and export functionality
    /// Provides abstraction around the type of the Race events
    /// For Example Horse Racing, Grey Hound Racing etc.
    /// </summary>
    /// <typeparam name="TSource">The Type of Source Data Object</typeparam>
    /// <typeparam name="TTarget">The type of Target Data Object</typeparam>
    public interface IRaceUpdateFileHandler<TSource, TTarget>
        where TSource : BaseRawRaceUpdate
        where TTarget : BaseRaceUpdate
    {
        /// <summary>
        /// Import from a url and deserialize to a certain data type
        /// </summary>
        /// <param name="url">Source url</param>
        /// <returns>Deserialized data</returns>
        public Task<TSource> Import(string url);

        /// <summary>
        /// Serialize a typed data and export to a url
        /// </summary>
        /// <param name="raceUpdate">Data</param>
        /// <param name="url">Export Url</param>
        /// <returns></returns>
        public Task Export(TTarget raceUpdate, string url);
    }
}
