namespace ApexToolsLauncher.Core.Extensions;

public static class ByteExtensions
{
    public static IEnumerable<byte> ReverseBytes(this IEnumerable<byte> value)
    {
        return value.Reverse();
    }

    public static ushort ReverseEndian(this ushort value)
    {
        return (ushort)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);
    }
        
    public static uint ReverseEndian(this uint value)
    {
        return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 08 |
               (value & 0x00FF0000U) >> 08 | (value & 0xFF000000U) >> 24;
    }
        
    public static ulong ReverseEndian(this ulong value)
    {
        return (value & 0x00000000000000FFUL) << 56 | (value & 0x000000000000FF00UL) << 40 |
               (value & 0x0000000000FF0000UL) << 24 | (value & 0x00000000FF000000UL) << 08 |
               (value & 0x000000FF00000000UL) >> 08 | (value & 0x0000FF0000000000UL) >> 24 |
               (value & 0x00FF000000000000UL) >> 40 | (value & 0xFF00000000000000UL) >> 56;
    }
    
    public static long Align(long value, long align, bool force = false)
    {
        if (value == 0) return 0;
        if (align == 0) return value;
        
        var desiredAlignment = (align - (value % align)) % align;
        if (force && desiredAlignment == 0)
        {
            desiredAlignment = align;
        }

        return value + desiredAlignment;
    }
}