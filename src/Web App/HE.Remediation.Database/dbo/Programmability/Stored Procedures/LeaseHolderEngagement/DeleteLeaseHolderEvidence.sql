CREATE PROCEDURE [dbo].[DeleteLeaseHolderEvidence]
	@FileId uniqueidentifier
AS
BEGIN
	delete from dbo.ApplicationLeaseHolderEngagementFile
	where FileId = @FileId
END