// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Misc;

public sealed class Tips : IDatacenterObject
{
	public static string ModuleName =>
		"Tips";

	public required int Id { get; set; }

	public required int DescId { get; set; }

	public required string Desc { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		DescId = d2OClass.ReadFieldAsI18N(reader);
		Desc = d2OClass.ReadFieldAsI18NString(DescId);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, DescId);
	}
}
