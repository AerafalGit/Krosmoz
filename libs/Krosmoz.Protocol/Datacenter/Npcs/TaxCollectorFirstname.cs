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

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		FirstnameId = d2OClass.Fields[1].AsI18N(reader);
	}
}
