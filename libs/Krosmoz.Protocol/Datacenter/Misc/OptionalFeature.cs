// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Misc;

public sealed class OptionalFeature : IDatacenterObject<OptionalFeature>
{
	public static string ModuleName =>
		"OptionalFeatures";

	public required int Id { get; set; }

	public required string Keyword { get; set; }

	public static OptionalFeature Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new OptionalFeature
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			Keyword = d2OClass.Fields[1].AsString(reader),
		};
	}
}
