




CREATE PROCEDURE [dbo].[GetBankAccountDetailsRepresentative]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT
        [BankAccountDetails].[NameOnTheAccount],
        [BankAccountDetails].[BankName],
        [BankAccountDetails].[BranchName],
        [BankAccountDetails].[AccountNumber],
        [BankAccountDetails].[SortCode]
    FROM
		[ApplicationDetails]
		LEFT JOIN [ApplicationBankDetails] ON [ApplicationBankDetails].[Id] = [ApplicationDetails].[BankDetailsId]
		LEFT JOIN [BankAccountDetails] ON [BankAccountDetails].[Id] = [ApplicationBankDetails].[RepresentativeBankAccountDetailsId]
    WHERE
		[ApplicationDetails].[Id] = @ApplicationId
END