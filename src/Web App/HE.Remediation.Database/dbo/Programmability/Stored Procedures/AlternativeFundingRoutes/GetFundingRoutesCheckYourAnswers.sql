
CREATE PROCEDURE [dbo].[GetFundingRoutesCheckYourAnswers]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	IF EXISTS (SELECT [Id] FROM [ApplicationDetails] WHERE [ApplicationDetails].[Id] = @ApplicationId)
		BEGIN
			  DECLARE @FundingStillPursuingAnswer VARCHAR(8000) 
			  DECLARE @AlternateFundingId UNIQUEIDENTIFIER = (SELECT [AlternateFundingId] FROM ApplicationDetails WHERE Id = @ApplicationId)

			  SELECT @FundingStillPursuingAnswer = COALESCE(@FundingStillPursuingAnswer + ',  ', '') + [ApplicationFundingRoutesType].[Type] FROM [ApplicationFundingRoutes]
			  INNER JOIN [ApplicationFundingRoutesType] ON [ApplicationFundingRoutesType].Id = [ApplicationFundingRoutes].[FundingRoutesTypeId]
			  WHERE [ApplicationFundingRoutes].[AlternateFundingId] = @AlternateFundingId

			  SELECT [ApplicationOtherSourcesPursuedType].[Type] AS PursuedSourcesFundingAnswer, @FundingStillPursuingAnswer AS FundingStillPursuingAnswer FROM [ApplicationAlternateFunding]
			  INNER JOIN [ApplicationOtherSourcesPursuedType] ON [ApplicationOtherSourcesPursuedType].Id = [ApplicationAlternateFunding].[OtherSourcesPursuedTypeId]
			  WHERE [ApplicationAlternateFunding].Id = @AlternateFundingId
		END
END