

CREATE PROCEDURE [dbo].[GetApplicationTaskListSummary]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT
		[ApplicationDetails].[ReferenceNumber] AS [ApplicationReferenceNumber],
        ISNULL([ApplicationDetails].[StatusId], 1) AS [ApplicationStatusId],
		ISNULL([ApplicationLeaseHolderEngagement].[TaskStatusId], 1) AS [ApplicationLeaseHolderEngagementStatusId],
		ISNULL([ApplicationBuildingDetails].[TaskStatusId], 1) AS [ApplicationBuildingDetailsStatusId],
		ISNULL([ApplicationResponsibleEntity].[TaskStatusId], 1) AS [ApplicationResponsibleEntityStatusId],
		ISNULL([ApplicationAlternateFunding].[TaskStatusId], 1) AS [ApplicationFundingRoutesStatusId],
		ISNULL([ApplicationBankDetails].[TaskStatusId], 1) AS [ApplicationBankDetailsStatusId],
		[ApplicationDetails].[ConfirmDeclaration],
		ISNULL([ApplicationFireRiskAssessment].[TaskStatusId], 1) AS [ApplicationFireRiskAssessmentStatusId]
    FROM
        [ApplicationDetails]
		LEFT JOIN [ApplicationLeaseHolderEngagement] ON [ApplicationLeaseHolderEngagement].[Id] = [ApplicationDetails].[LeaseHolderEngagementId]
		LEFT JOIN [ApplicationBuildingDetails] ON [ApplicationBuildingDetails].[Id] = [ApplicationDetails].[BuildingDetailsId]
		LEFT JOIN [ApplicationResponsibleEntity] ON [ApplicationResponsibleEntity].[Id] = [ApplicationDetails].[ResponsibleEntityId]
		LEFT JOIN [ApplicationAlternateFunding] ON [ApplicationAlternateFunding].[Id] = [ApplicationDetails].[AlternateFundingId]
		LEFT JOIN [ApplicationBankDetails] ON [ApplicationBankDetails].[Id] = [ApplicationDetails].[BankDetailsId]
		LEFT JOIN [ApplicationFireRiskAssessment] ON [ApplicationFireRiskAssessment].[Id] = [ApplicationDetails].[FireRiskAssessmentId]
    WHERE
		[ApplicationDetails].[Id] = @ApplicationId
END