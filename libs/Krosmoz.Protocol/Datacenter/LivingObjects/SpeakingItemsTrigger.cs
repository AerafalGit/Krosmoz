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
		TriggersId = d2OClass.Fields[0].AsInt(reader);
		TextIds = d2OClass.Fields[1].AsList(reader, static (field, r) => field.AsInt(r));
		States = d2OClass.Fields[2].AsList(reader, static (field, r) => field.AsInt(r));
	}
}
