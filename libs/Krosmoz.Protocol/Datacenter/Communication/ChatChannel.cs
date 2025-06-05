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

	public required string Name { get; set; }

	public required int DescriptionId { get; set; }

	public required string Description { get; set; }

	public required string Shortcut { get; set; }

	public required string ShortcutKey { get; set; }

	public required bool IsPrivate { get; set; }

	public required bool AllowObjects { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		DescriptionId = d2OClass.ReadFieldAsI18N(reader);
		Description = d2OClass.ReadFieldAsI18NString(DescriptionId);
		Shortcut = d2OClass.ReadFieldAsString(reader);
		ShortcutKey = d2OClass.ReadFieldAsString(reader);
		IsPrivate = d2OClass.ReadFieldAsBoolean(reader);
		AllowObjects = d2OClass.ReadFieldAsBoolean(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsI18N(writer, DescriptionId);
		d2OClass.WriteFieldAsString(writer, Shortcut);
		d2OClass.WriteFieldAsString(writer, ShortcutKey);
		d2OClass.WriteFieldAsBoolean(writer, IsPrivate);
		d2OClass.WriteFieldAsBoolean(writer, AllowObjects);
	}
}
