// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Documents;

public sealed class Document : IDatacenterObject<Document>
{
	public static string ModuleName =>
		"Documents";

	public required int Id { get; set; }

	public required int TypeId { get; set; }

	public required int TitleId { get; set; }

	public required int AuthorId { get; set; }

	public required int SubTitleId { get; set; }

	public required int ContentId { get; set; }

	public required string ContentCSS { get; set; }

	public static Document Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new Document
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			TypeId = d2OClass.Fields[1].AsInt(reader),
			TitleId = d2OClass.Fields[2].AsI18N(reader),
			AuthorId = d2OClass.Fields[3].AsI18N(reader),
			SubTitleId = d2OClass.Fields[4].AsI18N(reader),
			ContentId = d2OClass.Fields[5].AsI18N(reader),
			ContentCSS = d2OClass.Fields[6].AsString(reader),
		};
	}
}
