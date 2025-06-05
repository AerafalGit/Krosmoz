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
		Id = d2OClass.ReadFieldAsInt(reader);
		ListId = d2OClass.ReadFieldAsInt(reader);
		Language = d2OClass.ReadFieldAsString(reader);
		Word = d2OClass.ReadFieldAsString(reader);
		DeepLooking = d2OClass.ReadFieldAsBoolean(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsInt(writer, ListId);
		d2OClass.WriteFieldAsString(writer, Language);
		d2OClass.WriteFieldAsString(writer, Word);
		d2OClass.WriteFieldAsBoolean(writer, DeepLooking);
	}
}
