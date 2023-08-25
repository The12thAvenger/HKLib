using System.Xml.Linq;
using HKLib.Reflection.hk2018;

namespace HKLib.Serialization.hk2018.Xml.FormatHandlers;

public static class StringFormatHandler
{
    public static object Read(XElement element, HavokType type, XmlDeserializeContext context)
    {
        if (element.Attribute("value")?.Value is not { } stringValue)
        {
            throw new InvalidDataException("Missing \"value\" attribute for string value.");
        }

        return stringValue;
    }

    public static void Write(XElement parentElement, HavokType type, object? value, XmlSerializeContext context)
    {
        string stringVal = (string?)value ?? string.Empty;
        parentElement.Add(new XElement("string", new XAttribute("value", stringVal)));
    }
}