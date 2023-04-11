
CREATE PROCEDURE [dbo].[GetBankAccountDetailsResponsibleEntity]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT
        [BankAccountDetails].[NameOnTheAccount],
        [BankAccountDetails].[BankName],
        [BankAccountDetails].[BranchName],
        [BankAccountDetails].[AccountNumber],
        [BankAccountDetails].[SortCode],
        [ApplicationResponsibleEntity].[RepresentationTypeId] AS [RepresentationType]
    FROM
		[ApplicationDetails]
		LEFT JOIN [ApplicationBankDetails] ON [ApplicationBankDetails].[Id] = [ApplicationDetails].[BankDetailsId]
		LEFT JOIN [BankAccountDetails] ON [BankAccountDetails].[Id] = [ApplicationBankDetails].[ResponsibleEntityBankAccountDetailsId]
        LEFT JOIN [ApplicationResponsibleEntity] ON [ApplicationResponsibleEntity].[Id] = [ApplicationDetails].[ResponsibleEntityId]
    WHERE
		[ApplicationDetails].[Id] = @ApplicationId
END