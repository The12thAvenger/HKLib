using HKLib.hk2018;
using HKLib.Reflection.hk2018;

namespace HKLib.Serialization.hk2018.Xml;

public class XmlSerializeContext
{
    private int _currentPointerNumber = 1;
    private int _currentTypeId = 1;
    private Dictionary<object, string> _pointers = new();
    private Dictionary<HavokType, string> _typeIds = new();

    public XmlSerializeContext(HavokTypeRegistry typeRegistry)
    {
        TypeRegistry = typeRegistry;
    }

    public HavokTypeRegistry TypeRegistry { get; }

    public string Enqueue(object havokObject)
    {
        if (_pointers.TryGetValue(havokObject, out string? pointer))
        {
            return pointer;
        }
    }

    public string GetTypeId(HavokType type)
    {
        if (_typeIds.TryGetValue(type, out string? typeId))
        {
            return typeId;
        }
    }
}