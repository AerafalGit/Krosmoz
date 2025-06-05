// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Sounds;

public sealed class SoundAnimation : IDatacenterObject
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

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		Label = d2OClass.ReadFieldAsString(reader);
		Name = d2OClass.ReadFieldAsString(reader);
		Filename = d2OClass.ReadFieldAsString(reader);
		Volume = d2OClass.ReadFieldAsInt(reader);
		Rolloff = d2OClass.ReadFieldAsInt(reader);
		AutomationDuration = d2OClass.ReadFieldAsInt(reader);
		AutomationVolume = d2OClass.ReadFieldAsInt(reader);
		AutomationFadeIn = d2OClass.ReadFieldAsInt(reader);
		AutomationFadeOut = d2OClass.ReadFieldAsInt(reader);
		NoCutSilence = d2OClass.ReadFieldAsBoolean(reader);
		StartFrame = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsString(writer, Label);
		d2OClass.WriteFieldAsString(writer, Name);
		d2OClass.WriteFieldAsString(writer, Filename);
		d2OClass.WriteFieldAsInt(writer, Volume);
		d2OClass.WriteFieldAsInt(writer, Rolloff);
		d2OClass.WriteFieldAsInt(writer, AutomationDuration);
		d2OClass.WriteFieldAsInt(writer, AutomationVolume);
		d2OClass.WriteFieldAsInt(writer, AutomationFadeIn);
		d2OClass.WriteFieldAsInt(writer, AutomationFadeOut);
		d2OClass.WriteFieldAsBoolean(writer, NoCutSilence);
		d2OClass.WriteFieldAsInt(writer, StartFrame);
	}
}
