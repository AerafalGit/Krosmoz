// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Communication;

public sealed class ChatChannel : IDatacenterObject
{
	public static string ModuleName =>
		"ChatChannels";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required int DescriptionId { get; set; }

	public required string Shortcut { get; set; }

	public required string ShortcutKey { get; set; }

	public required bool IsPrivate { get; set; }

	public required bool AllowObjects { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		NameId = d2OClass.Fields[1].AsI18N(reader);
		DescriptionId = d2OClass.Fields[2].AsI18N(reader);
		Shortcut = d2OClass.Fields[3].AsString(reader);
		ShortcutKey = d2OClass.Fields[4].AsString(reader);
		IsPrivate = d2OClass.Fields[5].AsBoolean(reader);
		AllowObjects = d2OClass.Fields[6].AsBoolean(reader);
	}
}
