// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Communication;

public sealed class InfoMessage : IDatacenterObject
{
	public static string ModuleName =>
		"InfoMessages";

	public required int TypeId { get; set; }

	public required int MessageId { get; set; }

	public required int TextId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		TypeId = d2OClass.Fields[0].AsInt(reader);
		MessageId = d2OClass.Fields[1].AsInt(reader);
		TextId = d2OClass.Fields[2].AsI18N(reader);
	}
}
