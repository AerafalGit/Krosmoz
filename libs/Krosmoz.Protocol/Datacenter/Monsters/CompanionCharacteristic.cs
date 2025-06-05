// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Monsters;

public sealed class CompanionCharacteristic : IDatacenterObject
{
	public static string ModuleName =>
		"CompanionCharacteristics";

	public required int Id { get; set; }

	public required int CaracId { get; set; }

	public required int CompanionId { get; set; }

	public required int Order { get; set; }

	public required int InitialValue { get; set; }

	public required int LevelPerValue { get; set; }

	public required int ValuePerLevel { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		CaracId = d2OClass.ReadFieldAsInt(reader);
		CompanionId = d2OClass.ReadFieldAsInt(reader);
		Order = d2OClass.ReadFieldAsInt(reader);
		InitialValue = d2OClass.ReadFieldAsInt(reader);
		LevelPerValue = d2OClass.ReadFieldAsInt(reader);
		ValuePerLevel = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsInt(writer, CaracId);
		d2OClass.WriteFieldAsInt(writer, CompanionId);
		d2OClass.WriteFieldAsInt(writer, Order);
		d2OClass.WriteFieldAsInt(writer, InitialValue);
		d2OClass.WriteFieldAsInt(writer, LevelPerValue);
		d2OClass.WriteFieldAsInt(writer, ValuePerLevel);
	}
}
