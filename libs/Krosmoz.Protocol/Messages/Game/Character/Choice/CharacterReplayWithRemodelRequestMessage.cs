// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Messages.Game.Character.Replay;
using Krosmoz.Protocol.Types.Game.Character.Choice;

namespace Krosmoz.Protocol.Messages.Game.Character.Choice;

public sealed class CharacterReplayWithRemodelRequestMessage : CharacterReplayRequestMessage
{
	public new const uint StaticProtocolId = 6551;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new CharacterReplayWithRemodelRequestMessage Empty =>
		new() { CharacterId = 0, Remodel = RemodelingInformation.Empty };

	public required RemodelingInformation Remodel { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		Remodel.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Remodel = RemodelingInformation.Empty;
		Remodel.Deserialize(reader);
	}
}
