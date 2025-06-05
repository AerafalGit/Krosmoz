// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Version;

public sealed class VersionExtended : Version
{
	public new const ushort StaticProtocolId = 393;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new VersionExtended Empty =>
		new() { BuildType = 0, Patch = 0, Revision = 0, Release = 0, Minor = 0, Major = 0, Install = 0, Technology = 0 };

	public required sbyte Install { get; set; }

	public required sbyte Technology { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(Install);
		writer.WriteInt8(Technology);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Install = reader.ReadInt8();
		Technology = reader.ReadInt8();
	}
}
