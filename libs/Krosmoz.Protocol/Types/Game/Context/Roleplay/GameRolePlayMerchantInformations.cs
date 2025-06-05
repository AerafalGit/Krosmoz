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

	public required sbyte SellType { get; set; }

	public required IEnumerable<HumanOption> Options { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(SellType);
		var optionsBefore = writer.Position;
		var optionsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Options)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			optionsCount++;
		}
		var optionsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, optionsBefore);
		writer.WriteInt16((short)optionsCount);
		writer.Seek(SeekOrigin.Begin, optionsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		SellType = reader.ReadInt8();
		var optionsCount = reader.ReadInt16();
		var options = new HumanOption[optionsCount];
		for (var i = 0; i < optionsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<HumanOption>(reader.ReadUInt16());
			entry.Deserialize(reader);
			options[i] = entry;
		}
		Options = options;
	}
}
