using System.Xml.Linq;
using HKLib.hk2018;
using HKLib.Reflection.hk2018;

namespace HKLib.Serialization.hk2018.Xml;

public class XmlDeserializeContext
{
    private Dictionary<string, object> _objects = new();
    private Dictionary<string, HavokType> _types = new();
    private Dictionary<string, XElement> _xmlObjects = new();

    public object GetObject(string pointer)
    {
        if (pointer == "object0")
        {
            throw new ArgumentException("Cannot get item for null pointer.", nameof(pointer));
        }

        if (_objects.TryGetValue(pointer, out object? havokObject))
        {
            return havokObject;
        }
    }


    public HavokType GetType(string typeId)
    {
        if (typeId == "type0")
        {
            throw new ArgumentException("Cannot get type for id 0.", nameof(typeId));
        }

        if (_types.TryGetValue(typeId, out HavokType? havokType))
        {
            return havokType;
        }
    }
}