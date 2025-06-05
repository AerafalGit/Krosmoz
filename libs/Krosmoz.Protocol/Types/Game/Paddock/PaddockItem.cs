// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;
using Krosmoz.Protocol.Types.Game.Mount;

namespace Krosmoz.Protocol.Types.Game.Paddock;

public sealed class PaddockItem : ObjectItemInRolePlay
{
	public new const ushort StaticProtocolId = 185;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new PaddockItem Empty =>
		new() { ObjectGID = 0, CellId = 0, Durability = ItemDurability.Empty };

	public required ItemDurability Durability { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		Durability.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Durability = ItemDurability.Empty;
		Durability.Deserialize(reader);
	}
}
