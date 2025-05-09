// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Appearance;

public sealed class SkinMapping : IDatacenterObject<SkinMapping>
{
	public static string ModuleName =>
		"SkinMappings";

	public required int Id { get; set; }

	public required int LowDefId { get; set; }

	public static SkinMapping Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new SkinMapping
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			LowDefId = d2OClass.Fields[1].AsInt(reader),
		};
	}
}
