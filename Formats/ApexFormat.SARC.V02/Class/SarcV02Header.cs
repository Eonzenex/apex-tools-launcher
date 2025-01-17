﻿using ApexToolsLauncher.Core.Class;
using CommunityToolkit.HighPerformance;
using RustyOptions;

namespace ApexFormat.SARC.V02.Class;

/// <summary>
/// Structure:
/// <br/>MagicLength - <see cref="uint"/>
/// <br/>Magic - <see cref="uint"/>
/// <br/>Version - <see cref="uint"/>
/// <br/>Size - <see cref="uint"/>
/// </summary>
public class SarcV02Header : ISizeOf
{
    public uint MagicLength = SarcV02HeaderLibrary.MagicLength;
    public uint Magic = SarcV02HeaderLibrary.Magic;
    public uint Version = SarcV02HeaderLibrary.Version;
    public uint Size;

    public static uint SizeOf()
    {
        return sizeof(uint) + // MagicLength
               sizeof(uint) + // Magic
               sizeof(uint) + // Version
               sizeof(uint); // Size
    }
}

public static class SarcV02HeaderLibrary
{
    public const int SizeOf = sizeof(uint) // MagicLength
                              + sizeof(uint) // Magic
                              + sizeof(uint) // Version
                              + sizeof(uint); // Size
    
    public const uint MagicLength = 0x04;
    public const uint Magic = 0x43524153; // "SARC"
    public const uint Version = 0x02;
    
    public static Option<SarcV02Header> ReadSarcV02Header(this Stream stream)
    {
        if (stream.Length - stream.Position < SarcV02Header.SizeOf())
        {
            return Option<SarcV02Header>.None;
        }
        
        var result = new SarcV02Header
        {
            MagicLength = stream.Read<uint>(),
            Magic = stream.Read<uint>(),
            Version = stream.Read<uint>(),
            Size = stream.Read<uint>()
        };

        if (result.MagicLength != MagicLength)
        {
            return Option<SarcV02Header>.None;
        }

        if (result.Magic != Magic)
        {
            return Option<SarcV02Header>.None;
        }

        if (result.Version != Version)
        {
            return Option<SarcV02Header>.None;
        }

        return Option.Some(result);
    }
}