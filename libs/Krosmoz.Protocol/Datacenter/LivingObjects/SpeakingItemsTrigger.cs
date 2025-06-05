// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.LivingObjects;

public sealed class SpeakingItemsTrigger : IDatacenterObject
{
	public static string ModuleName =>
		"SpeakingItemsTriggers";

	public required int TriggersId { get; set; }

	public required List<int> TextIds { get; set; }

	public required List<int> States { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		TriggersId = d2OClass.ReadFieldAsInt(reader);
		TextIds = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsInt(r));
		States = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsInt(r));
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, TriggersId);
		d2OClass.WriteFieldAsList(writer, TextIds, static (c, r, v) => c.WriteFieldAsInt(r, v));
		d2OClass.WriteFieldAsList(writer, States, static (c, r, v) => c.WriteFieldAsInt(r, v));
	}
}
