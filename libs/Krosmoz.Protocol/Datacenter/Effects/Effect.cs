// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Effects;

public sealed class Effect : IDatacenterObject
{
	public static string ModuleName =>
		"Effects";

	public required int Id { get; set; }

	public required int DescriptionId { get; set; }

	public required int IconId { get; set; }

	public required int Characteristic { get; set; }

	public required int Category { get; set; }

	public required string Operator { get; set; }

	public required bool ShowInTooltip { get; set; }

	public required bool UseDice { get; set; }

	public required bool ForceMinMax { get; set; }

	public required bool Boost { get; set; }

	public required bool Active { get; set; }

	public required bool ShowInSet { get; set; }

	public required int BonusType { get; set; }

	public required bool UseInFight { get; set; }

	public required int EffectPriority { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		DescriptionId = d2OClass.Fields[1].AsI18N(reader);
		IconId = d2OClass.Fields[2].AsInt(reader);
		Characteristic = d2OClass.Fields[3].AsInt(reader);
		Category = d2OClass.Fields[4].AsInt(reader);
		Operator = d2OClass.Fields[5].AsString(reader);
		ShowInTooltip = d2OClass.Fields[6].AsBoolean(reader);
		UseDice = d2OClass.Fields[7].AsBoolean(reader);
		ForceMinMax = d2OClass.Fields[8].AsBoolean(reader);
		Boost = d2OClass.Fields[9].AsBoolean(reader);
		Active = d2OClass.Fields[10].AsBoolean(reader);
		ShowInSet = d2OClass.Fields[11].AsBoolean(reader);
		BonusType = d2OClass.Fields[12].AsInt(reader);
		UseInFight = d2OClass.Fields[13].AsBoolean(reader);
		EffectPriority = d2OClass.Fields[14].AsInt(reader);
	}
}
