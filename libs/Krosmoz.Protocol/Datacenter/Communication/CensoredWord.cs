// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Communication;

public sealed class CensoredWord : IDatacenterObject
{
	public static string ModuleName =>
		"CensoredWords";

	public required int Id { get; set; }

	public required int ListId { get; set; }

	public required string Language { get; set; }

	public required string Word { get; set; }

	public required bool DeepLooking { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		ListId = d2OClass.Fields[1].AsInt(reader);
		Language = d2OClass.Fields[2].AsString(reader);
		Word = d2OClass.Fields[3].AsString(reader);
		DeepLooking = d2OClass.Fields[4].AsBoolean(reader);
	}
}
