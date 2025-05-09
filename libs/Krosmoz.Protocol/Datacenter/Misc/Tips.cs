// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Misc;

public sealed class Tips : IDatacenterObject
{
	public static string ModuleName =>
		"Tips";

	public required int Id { get; set; }

	public required int DescId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		DescId = d2OClass.Fields[1].AsI18N(reader);
	}
}
