// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Notifications;

public sealed class Notification : IDatacenterObject<Notification>
{
	public static string ModuleName =>
		"Notifications";

	public required int Id { get; set; }

	public required int TitleId { get; set; }

	public required int MessageId { get; set; }

	public required int IconId { get; set; }

	public required int TypeId { get; set; }

	public required string Trigger { get; set; }

	public static Notification Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new Notification
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			TitleId = d2OClass.Fields[1].AsI18N(reader),
			MessageId = d2OClass.Fields[2].AsI18N(reader),
			IconId = d2OClass.Fields[3].AsInt(reader),
			TypeId = d2OClass.Fields[4].AsInt(reader),
			Trigger = d2OClass.Fields[5].AsString(reader),
		};
	}
}
