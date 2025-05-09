// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Npcs;

public sealed class Npc : IDatacenterObject<Npc>
{
	public static string ModuleName =>
		"Npcs";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required List<List<int>> DialogMessages { get; set; }

	public required List<List<int>> DialogReplies { get; set; }

	public required List<uint> Actions { get; set; }

	public required int Gender { get; set; }

	public required string Look { get; set; }

	public required List<AnimFunNpcData> AnimFunList { get; set; }

	public static Npc Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new Npc
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			NameId = d2OClass.Fields[1].AsI18N(reader),
			DialogMessages = d2OClass.Fields[2].AsListOfList<int>(reader, static (field, r) => field.AsInt(r)),
			DialogReplies = d2OClass.Fields[3].AsListOfList<int>(reader, static (field, r) => field.AsInt(r)),
			Actions = d2OClass.Fields[4].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
			Gender = d2OClass.Fields[5].AsInt(reader),
			Look = d2OClass.Fields[6].AsString(reader),
			AnimFunList = d2OClass.Fields[7].AsList<AnimFunNpcData>(reader, static (field, r) => field.AsObject<AnimFunNpcData>(r)),
		};
	}
}
