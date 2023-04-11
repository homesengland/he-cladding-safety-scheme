CREATE PROCEDURE [dbo].[UpsertBankAccountDetailsResponsibleEntity]
	@ApplicationId UNIQUEIDENTIFIER,
	@NameOnTheAccount NVARCHAR(150),
	@BankName NVARCHAR(150),
	@BranchName NVARCHAR(150),
	@AccountNumber NVARCHAR(150),
	@SortCode NVARCHAR(150)
AS
BEGIN
	BEGIN TRANSACTION

		DECLARE @ApplicationBankDetailsId AS UNIQUEIDENTIFIER
		SELECT @ApplicationBankDetailsId = BankDetailsId FROM [ApplicationDetails] WHERE [ApplicationDetails].[Id] = @ApplicationId

		DECLARE @BankAccountDetailsId AS UNIQUEIDENTIFIER

		SELECT 
			@BankAccountDetailsId = [BankAccountDetails].[Id] 
		FROM [ApplicationBankDetails] 
			LEFT JOIN [BankAccountDetails] 
			ON [ApplicationBankDetails].[ResponsibleEntityBankAccountDetailsId] = [BankAccountDetails].[Id] 
		WHERE [ApplicationBankDetails].[Id] = @ApplicationBankDetailsId

		DECLARE @CompletedTaskStatusId AS INT
		SELECT @CompletedTaskStatusId = Id FROM TaskStatus WHERE Type= 'Completed' 

		IF (@ApplicationBankDetailsId IS NULL)
			BEGIN
				SET @ApplicationBankDetailsId = NEWID()  
				INSERT INTO [ApplicationBankDetails] (Id, TaskStatusId) 
				VALUES (@ApplicationBankDetailsId, @CompletedTaskStatusId)
				
				UPDATE [ApplicationDetails] 
				SET [BankDetailsId] = @ApplicationBankDetailsId 
				WHERE Id = @ApplicationId

				SET @BankAccountDetailsId = NEWID() 
				INSERT INTO [BankAccountDetails] (Id, NameOnTheAccount, BankName, BranchName, AccountNumber, SortCode) 
				VALUES (@BankAccountDetailsId, @NameOnTheAccount, @BankName, @BranchName, @AccountNumber, @SortCode)
				
				UPDATE [ApplicationBankDetails] 
				SET [ResponsibleEntityBankAccountDetailsId] = @BankAccountDetailsId 
				WHERE Id = @ApplicationBankDetailsId
			END
		ELSE
			BEGIN
				UPDATE [ApplicationBankDetails]
				SET [TaskStatusId] = @CompletedTaskStatusId
				WHERE [Id] = @ApplicationBankDetailsId

				SET @BankAccountDetailsId = ISNULL(@BankAccountDetailsId, NEWID())

				MERGE INTO [dbo].[BankAccountDetails] t
				USING(
					VALUES(@BankAccountDetailsId, @NameOnTheAccount, @BankName, @BranchName, @AccountNumber, @SortCode)
				) AS s([Id], [NameOnTheAccount], [BankName], [BranchName], [AccountNumber], [SortCode])
				ON t.[Id] = s.[Id]
				WHEN MATCHED THEN
					UPDATE SET
						t.[NameOnTheAccount] = s.[NameOnTheAccount],
						t.[BankName] = s.[BankName],
						t.[BranchName] = s.[BranchName],
						t.[AccountNumber] = s.[AccountNumber],
						t.[SortCode] = s.[SortCode]
				WHEN NOT MATCHED THEN
					INSERT ([Id], [NameOnTheAccount], [BankName], [BranchName], [AccountNumber], [SortCode])
					VALUES (s.[Id], s.[NameOnTheAccount], s.[BankName], s.[BranchName], s.[AccountNumber], s.[SortCode]);

				UPDATE [dbo].[ApplicationBankDetails]
				SET [ResponsibleEntityBankAccountDetailsId] = @BankAccountDetailsId
				WHERE [Id] = @ApplicationBankDetailsId AND [ResponsibleEntityBankAccountDetailsId] IS NULL
			END

	COMMIT TRANSACTION
END