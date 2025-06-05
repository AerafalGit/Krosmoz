// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;
using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Character;

public sealed class CharacterMinimalAllianceInformations : CharacterMinimalGuildInformations
{
	public new const ushort StaticProtocolId = 444;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new CharacterMinimalAllianceInformations Empty =>
		new() { Id = 0, Name = string.Empty, Level = 0, EntityLook = EntityLook.Empty, Guild = BasicGuildInformations.Empty, Alliance = BasicAllianceInformations.Empty };

	public required BasicAllianceInformations Alliance { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		Alliance.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Alliance = BasicAllianceInformations.Empty;
		Alliance.Deserialize(reader);
	}
}
