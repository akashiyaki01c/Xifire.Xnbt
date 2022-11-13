using System;

namespace Xifire.Xnbt;

/// <summary>
/// Span構造体からXnbtタグを読み取る構造体
/// </summary>
public readonly ref struct TagReader
{

    private readonly Span<byte> _bytes;


    public TagReader(Span<byte> bytes)
    {
        if (bytes.Length < 2)
            throw new ArgumentException("長さが足りません");
        _bytes = bytes;
    }

    /// <summary> タグの種類 </summary>
    public TagType Type => (TagType)_bytes[0];
    /// <summary> 名前の長さ </summary>
    public byte NameLength => _bytes[1];
    /// <summary> タグの名前 </summary>
    internal Span<byte> RawNameBytes => _bytes.Slice(2, NameLength);
    /// <summary> タグの値 </summary>
    internal Span<byte> RawValueBytes => _bytes.Slice(2 + NameLength);

    /// <summary> タグの名前を返す関数 </summary>
    public string GetNameString()
    {
        Span<char> chars = stackalloc char[NameLength];
        for (var i = 0; i < RawNameBytes.Length; i++)
        {
            chars[i] = (char)RawNameBytes[i];
        }
        return new string(chars);
    }
}