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
		Lang = d2OClass.Fields[0].AsString(reader);
		Type = d2OClass.Fields[1].AsInt(reader);
		OldValue = d2OClass.Fields[2].AsInt(reader);
		NewValue = d2OClass.Fields[3].AsInt(reader);
	}
}
