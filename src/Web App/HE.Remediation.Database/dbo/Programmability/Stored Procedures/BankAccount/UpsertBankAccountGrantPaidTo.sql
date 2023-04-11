





CREATE PROCEDURE [dbo].[UpsertBankAccountGrantPaidTo]
	@ApplicationId UNIQUEIDENTIFIER,
	@BankDetailsRelationship INT
AS
BEGIN
	DECLARE @BankDetailsId AS UNIQUEIDENTIFIER
	SELECT @BankDetailsId = BankDetailsId FROM [ApplicationDetails] WHERE [ApplicationDetails].[Id] = @ApplicationId

	DECLARE  @InProgressTaskStatusId AS INT
	SELECT @InProgressTaskStatusId = Id FROM TaskStatus WHERE Type= 'In Progress' 

	IF (@BankDetailsId IS NULL)
		BEGIN
			SET @BankDetailsId = NEWID()  
			INSERT INTO [ApplicationBankDetails] (Id, ResponsibleEntityRelationship, TaskStatusId) VALUES (@BankDetailsId, @BankDetailsRelationship, @InProgressTaskStatusId)
			UPDATE [ApplicationDetails] SET [ApplicationDetails].[BankDetailsId] = @BankDetailsId WHERE [Id] = @ApplicationId
		END
	ELSE
		BEGIN
			UPDATE [ApplicationBankDetails] SET [ApplicationBankDetails].[ResponsibleEntityRelationship] = @BankDetailsRelationship WHERE [ApplicationBankDetails].[Id] = @BankDetailsId
		END
END