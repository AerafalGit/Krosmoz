// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Servers;

public sealed class Server : IDatacenterObject<Server>
{
	public static string ModuleName =>
		"Servers";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required int CommentId { get; set; }

	public required double OpeningDate { get; set; }

	public required string Language { get; set; }

	public required int PopulationId { get; set; }

	public required int GameTypeId { get; set; }

	public required int CommunityId { get; set; }

	public required List<string> RestrictedToLanguages { get; set; }

	public static Server Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new Server
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			NameId = d2OClass.Fields[1].AsI18N(reader),
			CommentId = d2OClass.Fields[2].AsI18N(reader),
			OpeningDate = d2OClass.Fields[3].AsDouble(reader),
			Language = d2OClass.Fields[4].AsString(reader),
			PopulationId = d2OClass.Fields[5].AsInt(reader),
			GameTypeId = d2OClass.Fields[6].AsInt(reader),
			CommunityId = d2OClass.Fields[7].AsInt(reader),
			RestrictedToLanguages = d2OClass.Fields[8].AsList<string>(reader, static (field, r) => field.AsString(r)),
		};
	}
}
