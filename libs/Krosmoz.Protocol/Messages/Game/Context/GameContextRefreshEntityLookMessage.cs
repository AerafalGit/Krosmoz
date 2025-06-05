// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Messages.Game.Context;

public sealed class GameContextRefreshEntityLookMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5637;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameContextRefreshEntityLookMessage Empty =>
		new() { Id = 0, Look = EntityLook.Empty };

	public required int Id { get; set; }

	public required EntityLook Look { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(Id);
		Look.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadInt32();
		Look = EntityLook.Empty;
		Look.Deserialize(reader);
	}
}
