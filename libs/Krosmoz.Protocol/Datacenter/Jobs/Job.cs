// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Jobs;

public sealed class Job : IDatacenterObject
{
	public static string ModuleName =>
		"Jobs";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required int SpecializationOfId { get; set; }

	public required int IconId { get; set; }

	public required List<int> ToolIds { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		NameId = d2OClass.Fields[1].AsI18N(reader);
		SpecializationOfId = d2OClass.Fields[2].AsInt(reader);
		IconId = d2OClass.Fields[3].AsInt(reader);
		ToolIds = d2OClass.Fields[4].AsList(reader, static (field, r) => field.AsInt(r));
	}
}
