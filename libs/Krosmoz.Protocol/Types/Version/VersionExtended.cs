// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Version;

public sealed class VersionExtended : Version, IEquatable<VersionExtended>
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
		writer.WriteSByte(Install);
		writer.WriteSByte(Technology);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Install = reader.ReadSByte();
		Technology = reader.ReadSByte();
	}

    public bool Equals(VersionExtended? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Major == other.Major &&
               Minor == other.Minor &&
               Release == other.Release &&
               Revision == other.Revision &&
               Patch == other.Patch &&
               BuildType == other.BuildType &&
               Install == other.Install &&
               Technology == other.Technology;
    }

    public override bool Equals(object? obj)
    {
        return obj is VersionExtended other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Major, Minor, Release, Revision, Patch, BuildType, Install, Technology);
    }
}
