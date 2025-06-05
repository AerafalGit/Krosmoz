// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Sounds;

public sealed class SoundUi : IDatacenterObject
{
	public static string ModuleName =>
		"SoundUi";

	public required int Id { get; set; }

	public required string UiName { get; set; }

	public required string OpenFile { get; set; }

	public required string CloseFile { get; set; }

	public required List<List<SoundUi>> SubElements { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		UiName = d2OClass.ReadFieldAsString(reader);
		OpenFile = d2OClass.ReadFieldAsString(reader);
		CloseFile = d2OClass.ReadFieldAsString(reader);
		SubElements = d2OClass.ReadFieldAsListOfList<SoundUi>(reader, static (c, r) => c.ReadFieldAsObject<SoundUi>(r));
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsString(writer, UiName);
		d2OClass.WriteFieldAsString(writer, OpenFile);
		d2OClass.WriteFieldAsString(writer, CloseFile);
		d2OClass.WriteFieldAsListOfList<SoundUi>(writer, SubElements, static (c, r, v) => c.WriteFieldAsObject<SoundUi>(r, v));
	}
}
