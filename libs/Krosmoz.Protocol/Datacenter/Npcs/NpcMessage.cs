// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Npcs;

public sealed class NpcMessage : IDatacenterObject<NpcMessage>
{
	public static string ModuleName =>
		"NpcMessages";

	public required int Id { get; set; }

	public required int MessageId { get; set; }

	public static NpcMessage Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new NpcMessage
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			MessageId = d2OClass.Fields[1].AsI18N(reader),
		};
	}
}
