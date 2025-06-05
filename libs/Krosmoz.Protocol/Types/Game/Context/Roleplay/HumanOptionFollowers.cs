// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class HumanOptionFollowers : HumanOption
{
	public new const ushort StaticProtocolId = 410;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new HumanOptionFollowers Empty =>
		new() { FollowingCharactersLook = [] };

	public required IEnumerable<IndexedEntityLook> FollowingCharactersLook { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var followingCharactersLookBefore = writer.Position;
		var followingCharactersLookCount = 0;
		writer.WriteInt16(0);
		foreach (var item in FollowingCharactersLook)
		{
			item.Serialize(writer);
			followingCharactersLookCount++;
		}
		var followingCharactersLookAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, followingCharactersLookBefore);
		writer.WriteInt16((short)followingCharactersLookCount);
		writer.Seek(SeekOrigin.Begin, followingCharactersLookAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var followingCharactersLookCount = reader.ReadInt16();
		var followingCharactersLook = new IndexedEntityLook[followingCharactersLookCount];
		for (var i = 0; i < followingCharactersLookCount; i++)
		{
			var entry = IndexedEntityLook.Empty;
			entry.Deserialize(reader);
			followingCharactersLook[i] = entry;
		}
		FollowingCharactersLook = followingCharactersLook;
	}
}
