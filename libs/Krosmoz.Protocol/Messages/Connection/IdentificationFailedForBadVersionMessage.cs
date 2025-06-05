// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Version = Krosmoz.Protocol.Types.Version.Version;

namespace Krosmoz.Protocol.Messages.Connection;

public sealed class IdentificationFailedForBadVersionMessage : IdentificationFailedMessage
{
	public new const uint StaticProtocolId = 21;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new IdentificationFailedForBadVersionMessage Empty =>
		new() { Reason = 0, RequiredVersion = Version.Empty };

	public required Version RequiredVersion { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		RequiredVersion.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		RequiredVersion = Version.Empty;
		RequiredVersion.Deserialize(reader);
	}
}
