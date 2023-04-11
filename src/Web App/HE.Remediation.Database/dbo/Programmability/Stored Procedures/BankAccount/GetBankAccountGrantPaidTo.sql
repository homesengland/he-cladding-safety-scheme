



CREATE PROCEDURE [dbo].[GetBankAccountGrantPaidTo]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT
        [ApplicationDetails].[Id] AS ApplicationId,
        [ApplicationBankDetails].[ResponsibleEntityRelationship] AS [BankDetailsRelationship]
    FROM
		[ApplicationDetails]
		LEFT JOIN [ApplicationBankDetails] ON [ApplicationBankDetails].[Id] = [ApplicationDetails].[BankDetailsId]
    WHERE
		[ApplicationDetails].[Id] = @ApplicationId
END