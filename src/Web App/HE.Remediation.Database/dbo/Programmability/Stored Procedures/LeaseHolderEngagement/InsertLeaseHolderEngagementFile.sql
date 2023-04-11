CREATE PROCEDURE [dbo].[InsertLeaseHolderEngagementFile]
	@FileId uniqueidentifier,
	@LeaseHolderId uniqueidentifier
AS
	insert into dbo.ApplicationLeaseHolderEngagementFile ([FileId], [LeaseHolderEngagementId])
	values (@FileId, @LeaseHolderId)
