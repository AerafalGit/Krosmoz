// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Communication;

public sealed class Smiley : IDatacenterObject<Smiley>
{
	public static string ModuleName =>
		"Smileys";

	public required int Id { get; set; }

	public required int Order { get; set; }

	public required string GfxId { get; set; }

	public required bool ForPlayers { get; set; }

	public required List<string> Triggers { get; set; }

	public static Smiley Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new Smiley
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			Order = d2OClass.Fields[1].AsInt(reader),
			GfxId = d2OClass.Fields[2].AsString(reader),
			ForPlayers = d2OClass.Fields[3].AsBoolean(reader),
			Triggers = d2OClass.Fields[4].AsList<string>(reader, static (field, r) => field.AsString(r)),
		};
	}
}
