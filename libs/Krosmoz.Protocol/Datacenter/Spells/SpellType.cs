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

	public required string LongName { get; set; }

	public required int ShortNameId { get; set; }

	public required string ShortName { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		LongNameId = d2OClass.ReadFieldAsI18N(reader);
		LongName = d2OClass.ReadFieldAsI18NString(LongNameId);
		ShortNameId = d2OClass.ReadFieldAsI18N(reader);
		ShortName = d2OClass.ReadFieldAsI18NString(ShortNameId);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, LongNameId);
		d2OClass.WriteFieldAsI18N(writer, ShortNameId);
	}
}
