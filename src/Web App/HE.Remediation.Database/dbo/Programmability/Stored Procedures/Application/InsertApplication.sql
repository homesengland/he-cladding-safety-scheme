CREATE PROCEDURE [dbo].[InsertApplication]
	@UserId UNIQUEIDENTIFIER,
	@CompanyId UNIQUEIDENTIFIER,
	@StatusId INTEGER,
	@StageId INTEGER
	
AS
BEGIN
	BEGIN TRANSACTION
		DECLARE @InsertedIds TABLE (Id UNIQUEIDENTIFIER)
		DECLARE @InsertedId UNIQUEIDENTIFIER

		DECLARE @NextId TABLE(NextID INT);
		;WITH NextIDGenerator AS 
		(
		  SELECT TOP (1) NextID FROM dbo.ApplicationReferenceNumbers ORDER BY RowNumber
		)
		DELETE NextIDGenerator OUTPUT deleted.NextID INTO @NextId;

		INSERT INTO ApplicationDetails
			(
				Id,
				ReferenceNumber,
				UserId,
				CompanyId,
				Submitted,
				StatusId,
				StageId,
				CreationDate
			)
		OUTPUT INSERTED.Id INTO @InsertedIds
		VALUES
			(
				NEWID(),
				CONCAT('APP', (SELECT NextID FROM @NextId)),
				@UserId,
				@CompanyId,
				0,
				@StatusId,
				@StageId,
				GETDATE()
			)
		
		SET @InsertedId = (SELECT TOP 1 Id FROM @InsertedIds);

		INSERT INTO ApplicationStateHistory
			(
				Id,
				AppId,
				StatusId,
				StateChanged
			)
		VALUES
			(
				NEWID(),
				@InsertedId,
				@StatusId,
				GETDATE()

			)

		SELECT @InsertedId
	COMMIT TRANSACTION

END
