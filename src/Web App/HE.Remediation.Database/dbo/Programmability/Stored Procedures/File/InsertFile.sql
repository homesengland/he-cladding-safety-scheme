CREATE PROCEDURE [dbo].[InsertFile]
	@Id uniqueidentifier,
	@Extension nvarchar(10),
	@MimeType nvarchar(50),
	@Name nvarchar(100),
	@Size int
AS
	insert into dbo.[File]([Id], [Extension], [MimeType], [Name], [Size])
	values (@Id, @Extension, @MimeType, @Name, @Size)