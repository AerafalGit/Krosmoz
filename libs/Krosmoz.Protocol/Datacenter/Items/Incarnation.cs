// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Items;

public sealed class Incarnation : IDatacenterObject
{
	public static string ModuleName =>
		"Incarnation";

	public required int Id { get; set; }

	public required string LookMale { get; set; }

	public required string LookFemale { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		LookMale = d2OClass.ReadFieldAsString(reader);
		LookFemale = d2OClass.ReadFieldAsString(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsString(writer, LookMale);
		d2OClass.WriteFieldAsString(writer, LookFemale);
	}
}
