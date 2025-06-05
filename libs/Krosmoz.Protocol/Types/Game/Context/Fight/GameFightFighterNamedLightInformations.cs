// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class GameFightFighterNamedLightInformations : GameFightFighterLightInformations
{
	public new const ushort StaticProtocolId = 456;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameFightFighterNamedLightInformations Empty =>
		new() { Breed = 0, Level = 0, Wave = 0, Id = 0, Alive = false, Sex = false, Name = string.Empty };

	public required string Name { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUtfPrefixedLength16(Name);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Name = reader.ReadUtfPrefixedLength16();
	}
}
