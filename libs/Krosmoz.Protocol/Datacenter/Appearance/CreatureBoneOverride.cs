// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Appearance;

public sealed class CreatureBoneOverride : IDatacenterObject
{
	public static string ModuleName =>
		"CreatureBonesOverrides";

	public required int BoneId { get; set; }

	public required int CreatureBoneId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		BoneId = d2OClass.ReadFieldAsInt(reader);
		CreatureBoneId = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, BoneId);
		d2OClass.WriteFieldAsInt(writer, CreatureBoneId);
	}
}
