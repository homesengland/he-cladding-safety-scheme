CREATE PROCEDURE [UpdateBuildingPartOfDevelopment]
	@ApplicationId UNIQUEIDENTIFIER,
	@PartOfDevelopment BIT
AS
BEGIN
	IF (@PartOfDevelopment) = 1

		UPDATE
			[ApplicationBuildingDetails] 
		SET
			PartOfDevelopment = @PartOfDevelopment
		FROM
			[ApplicationDetails] AD
				INNER JOIN
			[ApplicationBuildingDetails] ABD
				ON AD.[BuildingDetailsId] = ABD.[Id]
		WHERE
			AD.[Id] = @ApplicationId

	ELSE

		UPDATE
			[ApplicationBuildingDetails] 
		SET
			[PartOfDevelopment] = @PartOfDevelopment,
			[NameOfDevelopment] = NULL
		FROM
			[ApplicationDetails] AD
				INNER JOIN
			[ApplicationBuildingDetails] ABD
				ON AD.[BuildingDetailsId] = ABD.[Id]
		WHERE
			AD.[Id] = @ApplicationId

END
GO