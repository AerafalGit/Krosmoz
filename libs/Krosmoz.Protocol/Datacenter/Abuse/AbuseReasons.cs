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

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		AbuseReasonId = d2OClass.Fields[0].AsInt(reader);
		Mask = d2OClass.Fields[1].AsInt(reader);
		ReasonTextId = d2OClass.Fields[2].AsI18N(reader);
	}
}
