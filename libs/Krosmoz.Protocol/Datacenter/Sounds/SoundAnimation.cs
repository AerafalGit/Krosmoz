// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Sounds;

public sealed class SoundAnimation : IDatacenterObject<SoundAnimation>
{
	public static string ModuleName =>
		"SoundBones";

	public required int Id { get; set; }

	public required string Label { get; set; }

	public required string Name { get; set; }

	public required string Filename { get; set; }

	public required int Volume { get; set; }

	public required int Rolloff { get; set; }

	public required int AutomationDuration { get; set; }

	public required int AutomationVolume { get; set; }

	public required int AutomationFadeIn { get; set; }

	public required int AutomationFadeOut { get; set; }

	public required bool NoCutSilence { get; set; }

	public required int StartFrame { get; set; }

	public static SoundAnimation Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new SoundAnimation
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			Label = d2OClass.Fields[1].AsString(reader),
			Name = d2OClass.Fields[2].AsString(reader),
			Filename = d2OClass.Fields[3].AsString(reader),
			Volume = d2OClass.Fields[4].AsInt(reader),
			Rolloff = d2OClass.Fields[5].AsInt(reader),
			AutomationDuration = d2OClass.Fields[6].AsInt(reader),
			AutomationVolume = d2OClass.Fields[7].AsInt(reader),
			AutomationFadeIn = d2OClass.Fields[8].AsInt(reader),
			AutomationFadeOut = d2OClass.Fields[9].AsInt(reader),
			NoCutSilence = d2OClass.Fields[10].AsBoolean(reader),
			StartFrame = d2OClass.Fields[11].AsInt(reader),
		};
	}
}
