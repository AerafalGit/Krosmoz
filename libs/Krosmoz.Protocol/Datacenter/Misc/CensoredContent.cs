// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Misc;

public sealed class CensoredContent : IDatacenterObject
{
	public static string ModuleName =>
		"CensoredContents";

	public required string Lang { get; set; }

	public required int Type { get; set; }

	public required int OldValue { get; set; }

	public required int NewValue { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Lang = d2OClass.ReadFieldAsString(reader);
		Type = d2OClass.ReadFieldAsInt(reader);
		OldValue = d2OClass.ReadFieldAsInt(reader);
		NewValue = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsString(writer, Lang);
		d2OClass.WriteFieldAsInt(writer, Type);
		d2OClass.WriteFieldAsInt(writer, OldValue);
		d2OClass.WriteFieldAsInt(writer, NewValue);
	}
}
