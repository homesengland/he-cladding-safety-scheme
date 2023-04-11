
CREATE PROCEDURE [dbo].[GetFundingStillPursuing]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @AlternateFundingId AS UNIQUEIDENTIFIER

	IF EXISTS (SELECT [Id] FROM [ApplicationDetails] WHERE [ApplicationDetails].[Id] = @ApplicationId)
		BEGIN
			  SELECT @AlternateFundingId = AlternateFundingId FROM [ApplicationDetails] WHERE [ApplicationDetails].[Id] = @ApplicationId

			  IF (@AlternateFundingId IS NOT NULL)
				BEGIN 
					SELECT [FundingRoutesTypeId] AS FundingStillPursuing FROM [ApplicationFundingRoutes]
					WHERE [AlternateFundingId] = @AlternateFundingId
				END
		END
END