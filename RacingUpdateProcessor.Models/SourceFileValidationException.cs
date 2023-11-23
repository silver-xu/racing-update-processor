namespace RacingUpdateProcessor.Models;

/// <summary>
/// High level exception to throw when the source file
/// failed schema validation
/// </summary>
public class SourceFileValidationException : Exception
{
    public SourceFileValidationException(string message) : base(message) { }
}
