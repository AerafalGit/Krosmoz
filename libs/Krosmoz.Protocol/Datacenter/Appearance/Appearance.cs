// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Appearance;

public sealed class Appearance : IDatacenterObject
{
	public static string ModuleName =>
		"Appearances";

	public required int Id { get; set; }

	public required int Type { get; set; }

	public required string Data { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		Type = d2OClass.Fields[1].AsInt(reader);
		Data = d2OClass.Fields[2].AsString(reader);
	}
}
