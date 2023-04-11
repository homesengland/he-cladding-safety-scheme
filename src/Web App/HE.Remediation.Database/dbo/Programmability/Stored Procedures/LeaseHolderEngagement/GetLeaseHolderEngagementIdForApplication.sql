CREATE PROCEDURE [dbo].[GetLeaseHolderEngagementIdForApplication]
	@ApplicationId uniqueidentifier
AS
	declare @LeaseHolderEngagementId uniqueidentifier;

	set @LeaseHolderEngagementId = (select [LeaseHolderEngagementId] from dbo.ApplicationDetails
	where [Id] = @ApplicationId)

	if @LeaseHolderEngagementId IS NULL
	begin 
		set @LeaseHolderEngagementId = NEWID();
		insert into dbo.ApplicationLeaseHolderEngagement ([Id], [TaskStatusId], [Acknowledged])
		values (@LeaseHolderEngagementId, 2, 0)

		update dbo.ApplicationDetails
		set LeaseHolderEngagementId = @LeaseHolderEngagementId
		where Id = @ApplicationId
	end

	select @LeaseHolderEngagementId
