// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Chat.Channel;

public sealed class EnabledChannelsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 892;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static EnabledChannelsMessage Empty =>
		new() { Channels = [], Disallowed = [] };

	public required IEnumerable<sbyte> Channels { get; set; }

	public required IEnumerable<sbyte> Disallowed { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var channelsBefore = writer.Position;
		var channelsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Channels)
		{
			writer.WriteInt8(item);
			channelsCount++;
		}
		var channelsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, channelsBefore);
		writer.WriteInt16((short)channelsCount);
		writer.Seek(SeekOrigin.Begin, channelsAfter);
		var disallowedBefore = writer.Position;
		var disallowedCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Disallowed)
		{
			writer.WriteInt8(item);
			disallowedCount++;
		}
		var disallowedAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, disallowedBefore);
		writer.WriteInt16((short)disallowedCount);
		writer.Seek(SeekOrigin.Begin, disallowedAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var channelsCount = reader.ReadInt16();
		var channels = new sbyte[channelsCount];
		for (var i = 0; i < channelsCount; i++)
		{
			channels[i] = reader.ReadInt8();
		}
		Channels = channels;
		var disallowedCount = reader.ReadInt16();
		var disallowed = new sbyte[disallowedCount];
		for (var i = 0; i < disallowedCount; i++)
		{
			disallowed[i] = reader.ReadInt8();
		}
		Disallowed = disallowed;
	}
}
