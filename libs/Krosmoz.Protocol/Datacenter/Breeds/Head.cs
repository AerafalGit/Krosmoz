// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Breeds;

public sealed class Head : IDatacenterObject
{
	public static string ModuleName =>
		"Heads";

	public required int Id { get; set; }

	public required string Skins { get; set; }

	public required string AssetId { get; set; }

	public required int Breed { get; set; }

	public required int Gender { get; set; }

	public required string Label { get; set; }

	public required int Order { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		Skins = d2OClass.ReadFieldAsString(reader);
		AssetId = d2OClass.ReadFieldAsString(reader);
		Breed = d2OClass.ReadFieldAsInt(reader);
		Gender = d2OClass.ReadFieldAsInt(reader);
		Label = d2OClass.ReadFieldAsString(reader);
		Order = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsString(writer, Skins);
		d2OClass.WriteFieldAsString(writer, AssetId);
		d2OClass.WriteFieldAsInt(writer, Breed);
		d2OClass.WriteFieldAsInt(writer, Gender);
		d2OClass.WriteFieldAsString(writer, Label);
		d2OClass.WriteFieldAsInt(writer, Order);
	}
}
