// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Misc;

public sealed class ActionDescription : IDatacenterObject
{
	public static string ModuleName =>
		"ActionDescriptions";

	public required int Id { get; set; }

	public required int TypeId { get; set; }

	public required string Name { get; set; }

	public required int DescriptionId { get; set; }

	public required string Description { get; set; }

	public required bool Trusted { get; set; }

	public required bool NeedInteraction { get; set; }

	public required int MaxUsePerFrame { get; set; }

	public required int MinimalUseInterval { get; set; }

	public required bool NeedConfirmation { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		TypeId = d2OClass.ReadFieldAsInt(reader);
		Name = d2OClass.ReadFieldAsString(reader);
		DescriptionId = d2OClass.ReadFieldAsI18N(reader);
		Description = d2OClass.ReadFieldAsI18NString(DescriptionId);
		Trusted = d2OClass.ReadFieldAsBoolean(reader);
		NeedInteraction = d2OClass.ReadFieldAsBoolean(reader);
		MaxUsePerFrame = d2OClass.ReadFieldAsInt(reader);
		MinimalUseInterval = d2OClass.ReadFieldAsInt(reader);
		NeedConfirmation = d2OClass.ReadFieldAsBoolean(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsInt(writer, TypeId);
		d2OClass.WriteFieldAsString(writer, Name);
		d2OClass.WriteFieldAsI18N(writer, DescriptionId);
		d2OClass.WriteFieldAsBoolean(writer, Trusted);
		d2OClass.WriteFieldAsBoolean(writer, NeedInteraction);
		d2OClass.WriteFieldAsInt(writer, MaxUsePerFrame);
		d2OClass.WriteFieldAsInt(writer, MinimalUseInterval);
		d2OClass.WriteFieldAsBoolean(writer, NeedConfirmation);
	}
}
