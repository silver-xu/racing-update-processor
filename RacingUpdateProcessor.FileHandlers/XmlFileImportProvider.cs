using Microsoft.Extensions.Logging;
using RacingUpdateProcessor.FileHandlers.Interfaces;
using RacingUpdateProcessor.Models;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace RacingUpdateProcessor.FileHandlers;

public class XmlFileImportProvider<T> : IImportProvider<T>
{
    private readonly ITextFileProvider _fileProvider;
    private readonly ILogger<XmlFileImportProvider<T>> _logger;

    public XmlFileImportProvider(ITextFileProvider fileProvider, ILogger<XmlFileImportProvider<T>> logger)
    {
        _fileProvider = fileProvider;
        _logger = logger;
    }

    public async Task<T> Import(string schemaPath, string url)
    {
        var xmlContent = await _fileProvider.Download(url);

        return ImportFromString(schemaPath, xmlContent);
    }

    public T ImportFromString(string schemaPath, string xmlContent)
    {
        var settings = new XmlReaderSettings();
        settings.Schemas.Add(null, schemaPath);
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationEventHandler += (object? sender, ValidationEventArgs args) =>
        {
            switch (args.Severity)
            {
                case XmlSeverityType.Error:
                    _logger.LogError($"Error: Error occured while validating Xml Document: {args.Message}");
                    throw new SourceFileValidationException(args.Message);
                case XmlSeverityType.Warning:
                    _logger.LogWarning($"Warning: Warning occured while validating Xml Document: {args.Message}");
                    break;
            }
        };

        var document = new XmlDocument();
        var xmlReader = XmlReader.Create(new StringReader(xmlContent), settings);

        document.Load(xmlReader);

        var serializer = new XmlSerializer(typeof(T));
        var reader = new StringReader(xmlContent);

        var result = serializer.Deserialize(reader);
        if (result is null)
        {
            throw new InvalidOperationException("Xml cannot be deserialized");
        }
        return (T)result;
    }
}
