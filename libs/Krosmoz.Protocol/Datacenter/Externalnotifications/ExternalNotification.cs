// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Externalnotifications;

public sealed class ExternalNotification : IDatacenterObject
{
	public static string ModuleName =>
		"ExternalNotifications";

	public required int Id { get; set; }

	public required int CategoryId { get; set; }

	public required int IconId { get; set; }

	public required int ColorId { get; set; }

	public required int DescriptionId { get; set; }

	public required bool DefaultEnable { get; set; }

	public required bool DefaultSound { get; set; }

	public required bool DefaultMultiAccount { get; set; }

	public required bool DefaultNotify { get; set; }

	public required string Name { get; set; }

	public required int MessageId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		CategoryId = d2OClass.Fields[1].AsInt(reader);
		IconId = d2OClass.Fields[2].AsInt(reader);
		ColorId = d2OClass.Fields[3].AsInt(reader);
		DescriptionId = d2OClass.Fields[4].AsI18N(reader);
		DefaultEnable = d2OClass.Fields[5].AsBoolean(reader);
		DefaultSound = d2OClass.Fields[6].AsBoolean(reader);
		DefaultMultiAccount = d2OClass.Fields[7].AsBoolean(reader);
		DefaultNotify = d2OClass.Fields[8].AsBoolean(reader);
		Name = d2OClass.Fields[9].AsString(reader);
		MessageId = d2OClass.Fields[10].AsI18N(reader);
	}
}
