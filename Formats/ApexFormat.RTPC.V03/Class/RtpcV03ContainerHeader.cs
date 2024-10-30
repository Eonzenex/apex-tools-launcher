﻿using System.Xml.Linq;
using ATL.Core.Class;
using CommunityToolkit.HighPerformance;
using RustyOptions;

namespace ApexFormat.RTPC.V03.Class;

/// <summary>
/// <para>A container and its contents are separate</para>
/// Structure:
/// <br/>NameHash - <see cref="uint"/>
/// <br/>Offset - <see cref="uint"/>
/// <br/>PropertyCount - <see cref="ushort"/>
/// <br/>ContainerCount - <see cref="ushort"/>
/// </summary>
public class RtpcV03ContainerHeader : ISizeOf
{
    public uint NameHash = 0;
    public uint Offset = 0;
    public ushort PropertyCount = 0;
    public ushort ContainerCount = 0;

    public static uint SizeOf()
    {
        return sizeof(uint) + // NameHash
               sizeof(uint) + // Offset
               sizeof(ushort) + // PropertyCount
               sizeof(ushort); // ContainerCount
    }
}

public static class RtpcV03ContainerHeaderHeaderExtensions
{
    public static Option<RtpcV03ContainerHeader> ReadRtpcV03ContainerHeader(this Stream stream)
    {
        if (stream.Length - stream.Position < RtpcV03ContainerHeader.SizeOf())
        {
            return Option<RtpcV03ContainerHeader>.None;
        }
        
        var result = new RtpcV03ContainerHeader
        {
            NameHash = stream.Read<uint>(),
            Offset = stream.Read<uint>(),
            PropertyCount = stream.Read<ushort>(),
            ContainerCount = stream.Read<ushort>(),
        };

        return Option.Some(result);
    }
    
    public static XElement WriteXElement(this RtpcV03ContainerHeader container)
    {
        var xe = new XElement("object");
        xe.SetAttributeValue("id", $"{container.NameHash:X8}");

        // foreach (var property in container.Properties)
        // {
        //     var cxe = property.WriteXElement();
        //     xe.Add(cxe);
        // }
        
        return xe;
    }
}