// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Flash.Geom;

public sealed class Point : IDatacenterObject
{
	public static string ModuleName =>
		"QuestObjectives";

	public required int X { get; set; }

	public required int Y { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		X = d2OClass.Fields[0].AsInt(reader);
		Y = d2OClass.Fields[1].AsInt(reader);
	}
}
