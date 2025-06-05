// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Monsters;

public sealed class AnimFunMonsterData : IDatacenterObject
{
	public static string ModuleName =>
		"Monsters";

	public required string AnimName { get; set; }

	public required int AnimWeight { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		AnimName = d2OClass.ReadFieldAsString(reader);
		AnimWeight = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsString(writer, AnimName);
		d2OClass.WriteFieldAsInt(writer, AnimWeight);
	}
}
