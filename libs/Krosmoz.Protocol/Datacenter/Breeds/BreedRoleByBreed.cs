// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Breeds;

public sealed class BreedRoleByBreed : IDatacenterObject
{
	public static string ModuleName =>
		"Breeds";

	public required int BreedId { get; set; }

	public required int RoleId { get; set; }

	public required int DescriptionId { get; set; }

	public required string Description { get; set; }

	public required int Value { get; set; }

	public required int Order { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		BreedId = d2OClass.ReadFieldAsInt(reader);
		RoleId = d2OClass.ReadFieldAsInt(reader);
		DescriptionId = d2OClass.ReadFieldAsI18N(reader);
		Description = d2OClass.ReadFieldAsI18NString(DescriptionId);
		Value = d2OClass.ReadFieldAsInt(reader);
		Order = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, BreedId);
		d2OClass.WriteFieldAsInt(writer, RoleId);
		d2OClass.WriteFieldAsI18N(writer, DescriptionId);
		d2OClass.WriteFieldAsInt(writer, Value);
		d2OClass.WriteFieldAsInt(writer, Order);
	}
}
