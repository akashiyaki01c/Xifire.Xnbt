using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Xifire.Xnbt;

public static class TagReaderExtensions
{

    private static byte Get1ByteValue(this TagReader reader) => reader.RawValueBytes[0];
    private static short Get2BytesValue(this TagReader reader) => BinaryPrimitives.ReadInt16LittleEndian(reader.RawValueBytes);
    private static int Get4BytesValue(this TagReader reader) => BinaryPrimitives.ReadInt32LittleEndian(reader.RawValueBytes);
    private static long Get8BytesValue(this TagReader reader) => BinaryPrimitives.ReadInt64LittleEndian(reader.RawValueBytes);


    public static sbyte GetSByteValue(this TagReader reader) => unchecked((sbyte)Get1ByteValue(reader));
    public static byte GetByteValue(this TagReader reader) => Get1ByteValue(reader);
    public static short GetInt16Value(this TagReader reader) => Get2BytesValue(reader);
    public static ushort GetUInt16Value(this TagReader reader) => unchecked((ushort)Get2BytesValue(reader));
    public static int GetInt32Value(this TagReader reader) => Get4BytesValue(reader);
    public static uint GetUInt32Value(this TagReader reader) => unchecked((uint)Get4BytesValue(reader));
    public static long GetInt64Value(this TagReader reader) => Get8BytesValue(reader);
    public static ulong GetUInt64Value(this TagReader reader) => unchecked((ulong)Get8BytesValue(reader));

    public static Int128 GetInt128Value(this TagReader reader) => new(GetUInt64Value(reader), GetUInt64Value(reader));
    public static UInt128 GetUInt128Value(this TagReader reader) => new(GetUInt64Value(reader), GetUInt64Value(reader));

    public static Half GetHalfValue(this TagReader reader)
    {
        var temp = Get2BytesValue(reader);
        return Unsafe.As<short, Half>(ref temp);
    }

    public static float GetSingleValue(this TagReader reader)
    {
        var temp = Get4BytesValue(reader);
        return Unsafe.As<int, float>(ref temp);
    }

    public static double GetDoubleValue(this TagReader reader)
    {
        var temp = Get8BytesValue(reader);
        return Unsafe.As<long, double>(ref temp);
    }

    public static char GetCharValue(this TagReader reader) => unchecked((char)GetInt16Value(reader));


}