// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.ExternalNotifications;

public sealed class ExternalNotification : IDatacenterObject
{
	public static string ModuleName =>
		"ExternalNotifications";

	public required int Id { get; set; }

	public required int CategoryId { get; set; }

	public required int IconId { get; set; }

	public required int ColorId { get; set; }

	public required int DescriptionId { get; set; }

	public required string Description { get; set; }

	public required bool DefaultEnable { get; set; }

	public required bool DefaultSound { get; set; }

	public required bool DefaultMultiAccount { get; set; }

	public required bool DefaultNotify { get; set; }

	public required string Name { get; set; }

	public required int MessageId { get; set; }

	public required string Message { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		CategoryId = d2OClass.ReadFieldAsInt(reader);
		IconId = d2OClass.ReadFieldAsInt(reader);
		ColorId = d2OClass.ReadFieldAsInt(reader);
		DescriptionId = d2OClass.ReadFieldAsI18N(reader);
		Description = d2OClass.ReadFieldAsI18NString(DescriptionId);
		DefaultEnable = d2OClass.ReadFieldAsBoolean(reader);
		DefaultSound = d2OClass.ReadFieldAsBoolean(reader);
		DefaultMultiAccount = d2OClass.ReadFieldAsBoolean(reader);
		DefaultNotify = d2OClass.ReadFieldAsBoolean(reader);
		Name = d2OClass.ReadFieldAsString(reader);
		MessageId = d2OClass.ReadFieldAsI18N(reader);
		Message = d2OClass.ReadFieldAsI18NString(MessageId);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsInt(writer, CategoryId);
		d2OClass.WriteFieldAsInt(writer, IconId);
		d2OClass.WriteFieldAsInt(writer, ColorId);
		d2OClass.WriteFieldAsI18N(writer, DescriptionId);
		d2OClass.WriteFieldAsBoolean(writer, DefaultEnable);
		d2OClass.WriteFieldAsBoolean(writer, DefaultSound);
		d2OClass.WriteFieldAsBoolean(writer, DefaultMultiAccount);
		d2OClass.WriteFieldAsBoolean(writer, DefaultNotify);
		d2OClass.WriteFieldAsString(writer, Name);
		d2OClass.WriteFieldAsI18N(writer, MessageId);
	}
}
