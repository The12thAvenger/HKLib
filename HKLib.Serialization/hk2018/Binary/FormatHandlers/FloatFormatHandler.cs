using System.Runtime.Intrinsics;
using HKLib.Reflection.hk2018;
using HKLib.Serialization.hk2018.Binary.Util;

namespace HKLib.Serialization.hk2018.Binary.FormatHandlers;

internal static class FloatFormatHandler
{
    private const int EndianMask = 0x100;

    public static object Read(HavokBinaryReader reader, HavokType type, BinaryDeserializeContext context)
    {
        int format = type.Format;
        bool bigEndian = (format & EndianMask) != 0;

        return (type.Size, format >> 16) switch
        {
            (2, _) => ReadHalf(reader, bigEndian),
            (4, _) => reader.ReadSingle(bigEndian),
            (8, _) => reader.ReadDouble(bigEndian),
            (16, 23) => ReadVector128Float(reader, bigEndian),
            (16, 52) => ReadVector128Double(reader, bigEndian),
            _ => throw new InvalidDataException("Unexpected float format")
        };
    }

    public static void Write(HavokBinaryWriter writer, HavokType type, object value,
        BinarySerializeContext context)
    {
        int format = type.Format;
        bool bigEndian = (format & EndianMask) != 0;
        switch (type.Size, format >> 16)
        {
            case (2, _):
                WriteHalf(writer, bigEndian, value);
                break;
            case (4, _):
                writer.WriteSingle((float)value, bigEndian);
                break;
            case (8, _):
                writer.WriteDouble((double)value, bigEndian);
                break;
            case (16, 23):
                WriteVector128Float(writer, bigEndian, value);
                break;
            case (16, 52):
                WriteVector128Double(writer, bigEndian, value);
                break;
            default:
                throw new ArgumentException("Invalid float format", nameof(type));
        }
    }

    private static object ReadHalf(HavokBinaryReader reader, bool bigEndian)
    {
        byte[] bytes = new byte[4];
        reader.ReadBytes(bytes, bigEndian ? 0 : 2, 2);
        if (bigEndian)
        {
            Array.Reverse(bytes);
        }

        return BitConverter.ToSingle(bytes);
    }

    private static object ReadVector128Float(HavokBinaryReader reader, bool bigEndian)
    {
        float m1 = reader.ReadSingle(bigEndian);
        float m2 = reader.ReadSingle(bigEndian);
        float m3 = reader.ReadSingle(bigEndian);
        float m4 = reader.ReadSingle(bigEndian);

        return Vector128.Create(m1, m2, m3, m4);
    }

    private static object ReadVector128Double(HavokBinaryReader reader, bool bigEndian)
    {
        double m1 = reader.ReadDouble(bigEndian);
        double m2 = reader.ReadDouble(bigEndian);

        return Vector128.Create(m1, m2);
    }

    private static void WriteHalf(HavokBinaryWriter writer, bool bigEndian, object value)
    {
        float floatValue = (float)value;
        Span<byte> bytes = stackalloc byte[4];
        BitConverter.TryWriteBytes(bytes, floatValue);
        if (bigEndian)
        {
            writer.WriteByte(bytes[3]);
            writer.WriteByte(bytes[2]);
        }
        else
        {
            writer.WriteByte(bytes[2]);
            writer.WriteByte(bytes[3]);
        }
    }

    private static void WriteVector128Float(HavokBinaryWriter writer, bool bigEndian, object value)
    {
        Vector128<float> vectorVal = (Vector128<float>)value;

        writer.WriteSingle(vectorVal.GetElement(0), bigEndian);
        writer.WriteSingle(vectorVal.GetElement(1), bigEndian);
        writer.WriteSingle(vectorVal.GetElement(2), bigEndian);
        writer.WriteSingle(vectorVal.GetElement(3), bigEndian);
    }

    private static void WriteVector128Double(HavokBinaryWriter writer, bool bigEndian, object value)
    {
        Vector128<double> vectorVal = (Vector128<double>)value;
        writer.WriteDouble(vectorVal.GetElement(0), bigEndian);
        writer.WriteDouble(vectorVal.GetElement(1), bigEndian);
    }
}