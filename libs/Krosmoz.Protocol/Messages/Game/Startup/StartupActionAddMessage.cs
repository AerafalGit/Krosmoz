// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Startup;

namespace Krosmoz.Protocol.Messages.Game.Startup;

public sealed class StartupActionAddMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6538;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static StartupActionAddMessage Empty =>
		new() { NewAction = StartupActionAddObject.Empty };

	public required StartupActionAddObject NewAction { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		NewAction.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		NewAction = StartupActionAddObject.Empty;
		NewAction.Deserialize(reader);
	}
}
