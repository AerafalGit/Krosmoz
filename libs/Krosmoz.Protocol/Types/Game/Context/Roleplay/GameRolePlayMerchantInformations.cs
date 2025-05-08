// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class GameRolePlayMerchantInformations : GameRolePlayNamedActorInformations
{
	public new const ushort StaticProtocolId = 129;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameRolePlayMerchantInformations Empty =>
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, Name = string.Empty, SellType = 0, Options = [] };

	public required int SellType { get; set; }

	public required IEnumerable<HumanOption> Options { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(SellType);
		var optionsBefore = writer.Position;
		var optionsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Options)
		{
			writer.WriteUShort(item.ProtocolId);
			item.Serialize(writer);
			optionsCount++;
		}
		var optionsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, optionsBefore);
		writer.WriteShort((short)optionsCount);
		writer.Seek(SeekOrigin.Begin, optionsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		SellType = reader.ReadInt();
		var optionsCount = reader.ReadShort();
		var options = new HumanOption[optionsCount];
		for (var i = 0; i < optionsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<HumanOption>(reader.ReadUShort());
			entry.Deserialize(reader);
			options[i] = entry;
		}
		Options = options;
	}
}
