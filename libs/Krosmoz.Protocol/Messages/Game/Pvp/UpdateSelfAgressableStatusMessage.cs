// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Pvp;

public sealed class UpdateSelfAgressableStatusMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6456;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static UpdateSelfAgressableStatusMessage Empty =>
		new() { Status = 0, ProbationTime = 0 };

	public required sbyte Status { get; set; }

	public required int ProbationTime { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(Status);
		writer.WriteInt32(ProbationTime);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Status = reader.ReadInt8();
		ProbationTime = reader.ReadInt32();
	}
}
