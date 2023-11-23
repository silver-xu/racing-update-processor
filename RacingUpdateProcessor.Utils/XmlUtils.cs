using System.Xml.Serialization;

namespace RacingUpdateProcessor.Utils;

public static class XmlUtils
{
    public static string ToXmlString<T>(T obj) where T : class
    {
        var serializer = new XmlSerializer(typeof(T));

        using var textWriter = new StringWriter();
        serializer.Serialize(textWriter, obj);
        return textWriter.ToString();

    }
}
