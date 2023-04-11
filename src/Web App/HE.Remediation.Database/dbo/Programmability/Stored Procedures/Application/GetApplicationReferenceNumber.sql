
CREATE PROCEDURE [dbo].[GetApplicationReferenceNumber]
    @ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT TOP 1
        [ApplicationDetails].[ReferenceNumber]
    FROM
        [ApplicationDetails]
    WHERE
        [ApplicationDetails].[Id] = @ApplicationId
END