// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Communication;

public sealed class InfoMessage : IDatacenterObject
{
	public static string ModuleName =>
		"InfoMessages";

	public required int TypeId { get; set; }

	public required int MessageId { get; set; }

	public required int TextId { get; set; }

	public required string Text { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		TypeId = d2OClass.ReadFieldAsInt(reader);
		MessageId = d2OClass.ReadFieldAsInt(reader);
		TextId = d2OClass.ReadFieldAsI18N(reader);
		Text = d2OClass.ReadFieldAsI18NString(TextId);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, TypeId);
		d2OClass.WriteFieldAsInt(writer, MessageId);
		d2OClass.WriteFieldAsI18N(writer, TextId);
	}
}
