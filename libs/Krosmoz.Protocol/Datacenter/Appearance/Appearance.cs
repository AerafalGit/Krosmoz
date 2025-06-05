// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Appearance;

public sealed class Appearance : IDatacenterObject
{
	public static string ModuleName =>
		"Appearances";

	public required int Id { get; set; }

	public required int Type { get; set; }

	public required string Data { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		Type = d2OClass.ReadFieldAsInt(reader);
		Data = d2OClass.ReadFieldAsString(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsInt(writer, Type);
		d2OClass.WriteFieldAsString(writer, Data);
	}
}
