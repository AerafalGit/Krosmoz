// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Version;

public class Version : DofusType, IEquatable<Version>
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
		writer.WriteInt8(Major);
		writer.WriteInt8(Minor);
		writer.WriteInt8(Release);
		writer.WriteInt32(Revision);
		writer.WriteInt8(Patch);
		writer.WriteInt8(BuildType);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Major = reader.ReadInt8();
		Minor = reader.ReadInt8();
		Release = reader.ReadInt8();
		Revision = reader.ReadInt32();
		Patch = reader.ReadInt8();
		BuildType = reader.ReadInt8();
	}

    public bool Equals(Version? other)
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
               BuildType == other.BuildType;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is Version other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(BuildType, Patch, Revision, Release, Minor, Major);
    }
}
