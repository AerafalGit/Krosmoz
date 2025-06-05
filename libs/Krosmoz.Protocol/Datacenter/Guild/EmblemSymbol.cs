// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Guild;

public sealed class EmblemSymbol : IDatacenterObject
{
	public static string ModuleName =>
		"EmblemSymbols";

	public required int Id { get; set; }

	public required int SkinId { get; set; }

	public required int IconId { get; set; }

	public required int Order { get; set; }

	public required int CategoryId { get; set; }

	public required bool Colorizable { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		SkinId = d2OClass.ReadFieldAsInt(reader);
		IconId = d2OClass.ReadFieldAsInt(reader);
		Order = d2OClass.ReadFieldAsInt(reader);
		CategoryId = d2OClass.ReadFieldAsInt(reader);
		Colorizable = d2OClass.ReadFieldAsBoolean(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsInt(writer, SkinId);
		d2OClass.WriteFieldAsInt(writer, IconId);
		d2OClass.WriteFieldAsInt(writer, Order);
		d2OClass.WriteFieldAsInt(writer, CategoryId);
		d2OClass.WriteFieldAsBoolean(writer, Colorizable);
	}
}
