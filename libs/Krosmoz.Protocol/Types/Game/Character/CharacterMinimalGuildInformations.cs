// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;
using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Character;

public class CharacterMinimalGuildInformations : CharacterMinimalPlusLookInformations
{
	public new const ushort StaticProtocolId = 445;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new CharacterMinimalGuildInformations Empty =>
		new() { Id = 0, Name = string.Empty, Level = 0, EntityLook = EntityLook.Empty, Guild = BasicGuildInformations.Empty };

	public required BasicGuildInformations Guild { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		Guild.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Guild = BasicGuildInformations.Empty;
		Guild.Deserialize(reader);
	}
}
