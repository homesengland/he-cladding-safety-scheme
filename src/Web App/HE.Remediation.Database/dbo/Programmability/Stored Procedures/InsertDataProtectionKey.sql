CREATE PROCEDURE [dbo].[InsertDataProtectionKey]
	@FriendlyName NVARCHAR(MAX),
	@Xml NVARCHAR(MAX)
AS
BEGIN
	INSERT [DataProtectionKey] (FriendlyName, [Xml])
	VALUES (@FriendlyName, @Xml)
END
