// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Abuse;

public sealed class AbuseReasons : IDatacenterObject
{
	public static string ModuleName =>
		"AbuseReasons";

	public required int AbuseReasonId { get; set; }

	public required int Mask { get; set; }

	public required int ReasonTextId { get; set; }

	public required string ReasonText { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		AbuseReasonId = d2OClass.ReadFieldAsInt(reader);
		Mask = d2OClass.ReadFieldAsInt(reader);
		ReasonTextId = d2OClass.ReadFieldAsI18N(reader);
		ReasonText = d2OClass.ReadFieldAsI18NString(ReasonTextId);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, AbuseReasonId);
		d2OClass.WriteFieldAsInt(writer, Mask);
		d2OClass.WriteFieldAsI18N(writer, ReasonTextId);
	}
}
