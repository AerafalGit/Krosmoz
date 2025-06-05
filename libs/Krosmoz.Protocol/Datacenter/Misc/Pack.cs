// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Misc;

public sealed class Pack : IDatacenterObject
{
	public static string ModuleName =>
		"Pack";

	public required int Id { get; set; }

	public required string Name { get; set; }

	public required bool HasSubAreas { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		Name = d2OClass.ReadFieldAsString(reader);
		HasSubAreas = d2OClass.ReadFieldAsBoolean(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsString(writer, Name);
		d2OClass.WriteFieldAsBoolean(writer, HasSubAreas);
	}
}
