// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Sounds;

public sealed class SoundUi : IDatacenterObject<SoundUi>
{
	public static string ModuleName =>
		"SoundUi";

	public required int Id { get; set; }

	public required string UiName { get; set; }

	public required string OpenFile { get; set; }

	public required string CloseFile { get; set; }

	public required List<List<SoundUi>> SubElements { get; set; }

	public static SoundUi Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new SoundUi
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			UiName = d2OClass.Fields[1].AsString(reader),
			OpenFile = d2OClass.Fields[2].AsString(reader),
			CloseFile = d2OClass.Fields[3].AsString(reader),
			SubElements = d2OClass.Fields[4].AsListOfList<SoundUi>(reader, static (field, r) => field.AsObject<SoundUi>(r)),
		};
	}
}
