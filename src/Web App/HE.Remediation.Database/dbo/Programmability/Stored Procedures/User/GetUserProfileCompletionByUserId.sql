CREATE PROCEDURE [dbo].[GetUserProfileCompletionByUserId]
	@UserId UNIQUEIDENTIFIER
AS
BEGIN

	SELECT TOP 1
        UD.[ResponsibleEntityTypeId] AS [ResponsibleEntityType],
		UPC.[IsContactInformationComplete],
        UPC.[IsCorrespondenceAddressComplete],
        UPC.[IsResponsibleEntityTypeSelectionComplete], 
        UPC.[IsCompanyDetailsComplete], 
        UPC.[IsCompanyAddressComplete], 
        UPC.[IsSecondaryContactInformationComplete]
    FROM
        [UserProfileCompletion] UPC
    INNER JOIN
        [UserDetails] UD ON UD.[UserId] = UPC.[UserId]
    WHERE
        UPC.[UserId] = @UserId

END
