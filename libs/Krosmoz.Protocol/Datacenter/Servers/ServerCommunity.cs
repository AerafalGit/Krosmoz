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

	public required string Name { get; set; }

	public required List<string> DefaultCountries { get; set; }

	public required string ShortId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		DefaultCountries = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsString(r));
		ShortId = d2OClass.ReadFieldAsString(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsList(writer, DefaultCountries, static (c, r, v) => c.WriteFieldAsString(r, v));
		d2OClass.WriteFieldAsString(writer, ShortId);
	}
}
