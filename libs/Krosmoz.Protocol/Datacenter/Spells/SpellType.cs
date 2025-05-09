// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Spells;

public sealed class SpellType : IDatacenterObject
{
	public static string ModuleName =>
		"SpellTypes";

	public required int Id { get; set; }

	public required int LongNameId { get; set; }

	public required int ShortNameId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		LongNameId = d2OClass.Fields[1].AsI18N(reader);
		ShortNameId = d2OClass.Fields[2].AsI18N(reader);
	}
}
