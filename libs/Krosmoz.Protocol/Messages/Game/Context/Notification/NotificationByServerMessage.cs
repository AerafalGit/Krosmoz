// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Notification;

public sealed class NotificationByServerMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6103;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static NotificationByServerMessage Empty =>
		new() { Id = 0, Parameters = [], ForceOpen = false };

	public required ushort Id { get; set; }

	public required IEnumerable<string> Parameters { get; set; }

	public required bool ForceOpen { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(Id);
		var parametersBefore = writer.Position;
		var parametersCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Parameters)
		{
			writer.WriteUtfPrefixedLength16(item);
			parametersCount++;
		}
		var parametersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, parametersBefore);
		writer.WriteInt16((short)parametersCount);
		writer.Seek(SeekOrigin.Begin, parametersAfter);
		writer.WriteBoolean(ForceOpen);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadVarUInt16();
		var parametersCount = reader.ReadInt16();
		var parameters = new string[parametersCount];
		for (var i = 0; i < parametersCount; i++)
		{
			parameters[i] = reader.ReadUtfPrefixedLength16();
		}
		Parameters = parameters;
		ForceOpen = reader.ReadBoolean();
	}
}
