// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Breeds;

public sealed class Head : IDatacenterObject<Head>
{
	public static string ModuleName =>
		"Heads";

	public required int Id { get; set; }

	public required string Skins { get; set; }

	public required string AssetId { get; set; }

	public required int Breed { get; set; }

	public required int Gender { get; set; }

	public required string Label { get; set; }

	public required int Order { get; set; }

	public static Head Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new Head
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			Skins = d2OClass.Fields[1].AsString(reader),
			AssetId = d2OClass.Fields[2].AsString(reader),
			Breed = d2OClass.Fields[3].AsInt(reader),
			Gender = d2OClass.Fields[4].AsInt(reader),
			Label = d2OClass.Fields[5].AsString(reader),
			Order = d2OClass.Fields[6].AsInt(reader),
		};
	}
}
