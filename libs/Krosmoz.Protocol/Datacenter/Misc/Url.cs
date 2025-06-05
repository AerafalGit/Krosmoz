// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Misc;

public sealed class Url : IDatacenterObject
{
	public static string ModuleName =>
		"Url";

	public required int Id { get; set; }

	public required int BrowserId { get; set; }

	public required string Uri { get; set; }

	public required string Param { get; set; }

	public required string Method { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		BrowserId = d2OClass.ReadFieldAsInt(reader);
		Uri = d2OClass.ReadFieldAsString(reader);
		Param = d2OClass.ReadFieldAsString(reader);
		Method = d2OClass.ReadFieldAsString(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsInt(writer, BrowserId);
		d2OClass.WriteFieldAsString(writer, Uri);
		d2OClass.WriteFieldAsString(writer, Param);
		d2OClass.WriteFieldAsString(writer, Method);
	}
}
