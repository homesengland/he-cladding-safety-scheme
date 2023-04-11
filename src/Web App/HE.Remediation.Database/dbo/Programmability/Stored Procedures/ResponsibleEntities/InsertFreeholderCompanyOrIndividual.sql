CREATE PROCEDURE [dbo].[InsertFreeholderCompanyOrIndividual]
	@ApplicationId UNIQUEIDENTIFIER,
	@FreeholderId UNIQUEIDENTIFIER,
	@ResponsibleEntityTypeId INT
AS
BEGIN
	BEGIN TRANSACTION
		INSERT INTO ApplicationResponsibleEntityFreeholder
		(
			[Id],
			[ResponsibleEntityTypeId]
		)
		VALUES
		(
			@FreeholderId,
			@ResponsibleEntityTypeId
		);

		UPDATE
			[ApplicationResponsibleEntity]
		SET
			FreeholderId = @FreeholderId
		FROM
			[ApplicationDetails] AD
				INNER JOIN
			[ApplicationResponsibleEntity] ARE
				ON AD.[ResponsibleEntityId] = ARE.[Id]
		WHERE
			AD.[Id] = @ApplicationId;
	COMMIT TRANSACTION
END
GO