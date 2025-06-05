// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Job;

public sealed class JobCrafterDirectorySettings : DofusType
{
	public new const ushort StaticProtocolId = 97;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static JobCrafterDirectorySettings Empty =>
		new() { JobId = 0, MinLevel = 0, Free = false };

	public required sbyte JobId { get; set; }

	public required byte MinLevel { get; set; }

	public required bool Free { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(JobId);
		writer.WriteUInt8(MinLevel);
		writer.WriteBoolean(Free);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		JobId = reader.ReadInt8();
		MinLevel = reader.ReadUInt8();
		Free = reader.ReadBoolean();
	}
}
