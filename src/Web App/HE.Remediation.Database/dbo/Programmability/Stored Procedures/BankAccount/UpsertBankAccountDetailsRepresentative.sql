








CREATE PROCEDURE [dbo].[UpsertBankAccountDetailsRepresentative]
	@ApplicationId UNIQUEIDENTIFIER,
	@NameOnTheAccount NVARCHAR(150),
	@BankName NVARCHAR(150),
	@BranchName NVARCHAR(150),
	@AccountNumber NVARCHAR(150),
	@SortCode NVARCHAR(150)
AS
BEGIN
	DECLARE @ApplicationBankDetailsId AS UNIQUEIDENTIFIER
	SELECT @ApplicationBankDetailsId = BankDetailsId FROM [ApplicationDetails] WHERE [ApplicationDetails].[Id] = @ApplicationId

	DECLARE @BankAccountDetailsId AS UNIQUEIDENTIFIER
	SELECT @BankAccountDetailsId = [BankAccountDetails].[Id] FROM [ApplicationBankDetails] LEFT JOIN [BankAccountDetails] ON [ApplicationBankDetails].[RepresentativeBankAccountDetailsId] = [BankAccountDetails].[Id] WHERE [ApplicationBankDetails].[Id] = @ApplicationBankDetailsId

	DECLARE @CompletedTaskStatusId AS INT
	SELECT @CompletedTaskStatusId = Id FROM TaskStatus WHERE Type= 'Completed' 

	IF (@BankAccountDetailsId IS NULL)
		BEGIN
			SET @BankAccountDetailsId = NEWID()  
			INSERT INTO [BankAccountDetails] (Id, NameOnTheAccount, BankName, BranchName, AccountNumber, SortCode) VALUES (@BankAccountDetailsId, @NameOnTheAccount, @BankName, @BranchName, @AccountNumber, @SortCode)
			UPDATE [ApplicationBankDetails] SET [ApplicationBankDetails].[RepresentativeBankAccountDetailsId] = @BankAccountDetailsId, TaskStatusId = @CompletedTaskStatusId WHERE Id = @ApplicationBankDetailsId
		END
	ELSE
		BEGIN
			UPDATE [BankAccountDetails] SET
				[BankAccountDetails].[NameOnTheAccount] = @NameOnTheAccount,
				[BankAccountDetails].[BankName] = @BankName,
				[BankAccountDetails].[BranchName] = @BranchName,
				[BankAccountDetails].[AccountNumber] = @AccountNumber,
				[BankAccountDetails].[SortCode] = @SortCode
			WHERE 
				[BankAccountDetails].[Id] = @BankAccountDetailsId
		END
END