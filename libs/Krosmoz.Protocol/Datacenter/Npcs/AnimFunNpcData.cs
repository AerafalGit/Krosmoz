// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Npcs;

public sealed class AnimFunNpcData : IDatacenterObject
{
	public static string ModuleName =>
		"Npcs";

	public required string AnimName { get; set; }

	public required int AnimWeight { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		AnimName = d2OClass.Fields[0].AsString(reader);
		AnimWeight = d2OClass.Fields[1].AsInt(reader);
	}
}
