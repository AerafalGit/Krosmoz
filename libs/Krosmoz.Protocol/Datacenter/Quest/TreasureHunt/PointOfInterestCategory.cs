// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Quest.TreasureHunt;

public sealed class PointOfInterestCategory : IDatacenterObject
{
	public static string ModuleName =>
		"PointOfInterestCategory";

	public required int Id { get; set; }

	public required int ActionLabelId { get; set; }

	public required string ActionLabel { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		ActionLabelId = d2OClass.ReadFieldAsI18N(reader);
		ActionLabel = d2OClass.ReadFieldAsI18NString(ActionLabelId);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, ActionLabelId);
	}
}
