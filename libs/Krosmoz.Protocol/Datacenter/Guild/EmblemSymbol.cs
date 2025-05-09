// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Guild;

public sealed class EmblemSymbol : IDatacenterObject<EmblemSymbol>
{
	public static string ModuleName =>
		"EmblemSymbols";

	public required int Id { get; set; }

	public required int SkinId { get; set; }

	public required int IconId { get; set; }

	public required int Order { get; set; }

	public required int CategoryId { get; set; }

	public required bool Colorizable { get; set; }

	public static EmblemSymbol Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new EmblemSymbol
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			SkinId = d2OClass.Fields[1].AsInt(reader),
			IconId = d2OClass.Fields[2].AsInt(reader),
			Order = d2OClass.Fields[3].AsInt(reader),
			CategoryId = d2OClass.Fields[4].AsInt(reader),
			Colorizable = d2OClass.Fields[5].AsBoolean(reader),
		};
	}
}
