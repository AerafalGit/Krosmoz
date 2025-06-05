// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Misc;

public sealed class TypeAction : IDatacenterObject
{
	public static string ModuleName =>
		"TypeActions";

	public required int Id { get; set; }

	public required string ElementName { get; set; }

	public required int ElementId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		ElementName = d2OClass.ReadFieldAsString(reader);
		ElementId = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsString(writer, ElementName);
		d2OClass.WriteFieldAsInt(writer, ElementId);
	}
}
