// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Items;

public sealed class PresetIcon : IDatacenterObject<PresetIcon>
{
	public static string ModuleName =>
		"PresetIcons";

	public required int Id { get; set; }

	public required int Order { get; set; }

	public static PresetIcon Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new PresetIcon
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			Order = d2OClass.Fields[1].AsInt(reader),
		};
	}
}
