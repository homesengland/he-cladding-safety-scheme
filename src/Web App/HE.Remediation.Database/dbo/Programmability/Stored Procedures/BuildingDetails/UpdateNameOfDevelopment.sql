CREATE PROCEDURE [UpdateNameOfDevelopment]
	@ApplicationId UNIQUEIDENTIFIER,
	@NameOfDevelopment NVARCHAR(150)
AS
BEGIN
    UPDATE
		[ApplicationBuildingDetails] 
	SET
		NameOfDevelopment = @NameOfDevelopment
	FROM
		[ApplicationDetails] AD
			INNER JOIN
		[ApplicationBuildingDetails] ABD
			ON AD.[BuildingDetailsId] = ABD.[Id]
	WHERE
		AD.[Id] = @ApplicationId
END
GO