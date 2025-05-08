// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Version;

public class Version : DofusType
{
	public new const ushort StaticProtocolId = 11;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static Version Empty =>
		new() { Major = 0, Minor = 0, Release = 0, Revision = 0, Patch = 0, BuildType = 0 };

	public required sbyte Major { get; set; }

	public required sbyte Minor { get; set; }

	public required sbyte Release { get; set; }

	public required int Revision { get; set; }

	public required sbyte Patch { get; set; }

	public required sbyte BuildType { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(Major);
		writer.WriteSByte(Minor);
		writer.WriteSByte(Release);
		writer.WriteInt(Revision);
		writer.WriteSByte(Patch);
		writer.WriteSByte(BuildType);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Major = reader.ReadSByte();
		Minor = reader.ReadSByte();
		Release = reader.ReadSByte();
		Revision = reader.ReadInt();
		Patch = reader.ReadSByte();
		BuildType = reader.ReadSByte();
	}
}
