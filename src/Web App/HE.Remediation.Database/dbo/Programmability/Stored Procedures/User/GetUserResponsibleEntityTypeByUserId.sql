CREATE PROCEDURE [dbo].[GetUserResponsibleEntityTypeByUserId]
	@UserId UNIQUEIDENTIFIER
AS
    SELECT
        [ResponsibleEntityTypeId]     
    FROM
        [UserDetails]
    WHERE
        [UserId] = @UserId
