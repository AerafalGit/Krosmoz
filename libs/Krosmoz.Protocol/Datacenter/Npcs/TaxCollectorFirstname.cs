// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Npcs;

public sealed class TaxCollectorFirstname : IDatacenterObject
{
	public static string ModuleName =>
		"TaxCollectorFirstnames";

	public required int Id { get; set; }

	public required int FirstnameId { get; set; }

	public required string Firstname { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		FirstnameId = d2OClass.ReadFieldAsI18N(reader);
		Firstname = d2OClass.ReadFieldAsI18NString(FirstnameId);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, FirstnameId);
	}
}
