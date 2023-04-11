CREATE PROCEDURE [dbo].[GetDataProtectionKeys]
AS
BEGIN
	SELECT [Id], [FriendlyName], [Xml]
	FROM [DataProtectionKey]
END
