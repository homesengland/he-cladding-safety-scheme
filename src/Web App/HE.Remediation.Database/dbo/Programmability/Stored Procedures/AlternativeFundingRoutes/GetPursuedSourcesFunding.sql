
CREATE PROCEDURE [dbo].[GetPursuedSourcesFunding]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @AlternateFundingId AS UNIQUEIDENTIFIER

	IF EXISTS (SELECT [Id] FROM [ApplicationDetails] WHERE [ApplicationDetails].[Id] = @ApplicationId)
		BEGIN
			  SELECT @AlternateFundingId = AlternateFundingId FROM [ApplicationDetails] WHERE [ApplicationDetails].[Id] = @ApplicationId

			  IF (@AlternateFundingId IS NOT NULL)
				BEGIN 
					SELECT [OtherSourcesPursuedTypeId] AS PursuedSourcesFunding FROM [ApplicationAlternateFunding]
					WHERE [ApplicationAlternateFunding].[Id] = @AlternateFundingId
				END
		END
END