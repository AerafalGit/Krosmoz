// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Sounds;

public sealed class SoundUiElement : IDatacenterObject
{
	public static string ModuleName =>
		"SoundUi";

	public required int Id { get; set; }

	public required string Name { get; set; }

	public required int HookId { get; set; }

	public required string File { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		Name = d2OClass.Fields[1].AsString(reader);
		HookId = d2OClass.Fields[2].AsInt(reader);
		File = d2OClass.Fields[3].AsString(reader);
	}
}
