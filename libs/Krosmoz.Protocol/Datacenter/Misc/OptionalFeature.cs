// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Misc;

public sealed class OptionalFeature : IDatacenterObject
{
	public static string ModuleName =>
		"OptionalFeatures";

	public required int Id { get; set; }

	public required string Keyword { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		Keyword = d2OClass.ReadFieldAsString(reader);
	}
}
