// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Misc;

public sealed class TypeAction : IDatacenterObject<TypeAction>
{
	public static string ModuleName =>
		"TypeActions";

	public required int Id { get; set; }

	public required string ElementName { get; set; }

	public required int ElementId { get; set; }

	public static TypeAction Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new TypeAction
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			ElementName = d2OClass.Fields[1].AsString(reader),
			ElementId = d2OClass.Fields[2].AsInt(reader),
		};
	}
}
