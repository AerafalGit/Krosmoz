// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Servers;

public sealed class ServerCommunity : IDatacenterObject
{
	public static string ModuleName =>
		"ServerCommunities";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required List<string> DefaultCountries { get; set; }

	public required string ShortId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		NameId = d2OClass.Fields[1].AsI18N(reader);
		DefaultCountries = d2OClass.Fields[2].AsList<string>(reader, static (field, r) => field.AsString(r));
		ShortId = d2OClass.Fields[3].AsString(reader);
	}
}
