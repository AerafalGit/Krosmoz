// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Servers;

public sealed class Server : IDatacenterObject
{
	public static string ModuleName =>
		"Servers";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required int CommentId { get; set; }

	public required string Comment { get; set; }

	public required double OpeningDate { get; set; }

	public required string Language { get; set; }

	public required int PopulationId { get; set; }

	public required int GameTypeId { get; set; }

	public required int CommunityId { get; set; }

	public required List<string> RestrictedToLanguages { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		CommentId = d2OClass.ReadFieldAsI18N(reader);
		Comment = d2OClass.ReadFieldAsI18NString(CommentId);
		OpeningDate = d2OClass.ReadFieldAsNumber(reader);
		Language = d2OClass.ReadFieldAsString(reader);
		PopulationId = d2OClass.ReadFieldAsInt(reader);
		GameTypeId = d2OClass.ReadFieldAsInt(reader);
		CommunityId = d2OClass.ReadFieldAsInt(reader);
		RestrictedToLanguages = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsString(r));
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsI18N(writer, CommentId);
		d2OClass.WriteFieldAsNumber(writer, OpeningDate);
		d2OClass.WriteFieldAsString(writer, Language);
		d2OClass.WriteFieldAsInt(writer, PopulationId);
		d2OClass.WriteFieldAsInt(writer, GameTypeId);
		d2OClass.WriteFieldAsInt(writer, CommunityId);
		d2OClass.WriteFieldAsList(writer, RestrictedToLanguages, static (c, r, v) => c.WriteFieldAsString(r, v));
	}
}
