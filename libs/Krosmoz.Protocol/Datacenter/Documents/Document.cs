// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Documents;

public sealed class Document : IDatacenterObject
{
	public static string ModuleName =>
		"Documents";

	public required int Id { get; set; }

	public required int TypeId { get; set; }

	public required bool ShowTitle { get; set; }

	public required bool ShowBackgroundImage { get; set; }

	public required int TitleId { get; set; }

	public required string Title { get; set; }

	public required int AuthorId { get; set; }

	public required string Author { get; set; }

	public required int SubTitleId { get; set; }

	public required string SubTitle { get; set; }

	public required int ContentId { get; set; }

	public required string Content { get; set; }

	public required string ContentCSS { get; set; }

	public required string ClientProperties { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		TypeId = d2OClass.ReadFieldAsInt(reader);
		ShowTitle = d2OClass.ReadFieldAsBoolean(reader);
		ShowBackgroundImage = d2OClass.ReadFieldAsBoolean(reader);
		TitleId = d2OClass.ReadFieldAsI18N(reader);
		Title = d2OClass.ReadFieldAsI18NString(TitleId);
		AuthorId = d2OClass.ReadFieldAsI18N(reader);
		Author = d2OClass.ReadFieldAsI18NString(AuthorId);
		SubTitleId = d2OClass.ReadFieldAsI18N(reader);
		SubTitle = d2OClass.ReadFieldAsI18NString(SubTitleId);
		ContentId = d2OClass.ReadFieldAsI18N(reader);
		Content = d2OClass.ReadFieldAsI18NString(ContentId);
		ContentCSS = d2OClass.ReadFieldAsString(reader);
		ClientProperties = d2OClass.ReadFieldAsString(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsInt(writer, TypeId);
		d2OClass.WriteFieldAsBoolean(writer, ShowTitle);
		d2OClass.WriteFieldAsBoolean(writer, ShowBackgroundImage);
		d2OClass.WriteFieldAsI18N(writer, TitleId);
		d2OClass.WriteFieldAsI18N(writer, AuthorId);
		d2OClass.WriteFieldAsI18N(writer, SubTitleId);
		d2OClass.WriteFieldAsI18N(writer, ContentId);
		d2OClass.WriteFieldAsString(writer, ContentCSS);
		d2OClass.WriteFieldAsString(writer, ClientProperties);
	}
}
