CREATE PROCEDURE [dbo].[ScaffoldUserProfileCompletionData]
    @UserId UNIQUEIDENTIFIER
AS
BEGIN

    DECLARE @NewUserProfileCompletionID UNIQUEIDENTIFIER;

    SET @NewUserProfileCompletionID = NEWID();

    INSERT INTO [UserProfileCompletion]
    (
        [Id], 
        [UserId], 
        [IsContactInformationComplete], 
        [IsCorrespondenceAddressComplete],
        [IsResponsibleEntityTypeSelectionComplete], 
        [IsCompanyDetailsComplete], 
        [IsCompanyAddressComplete], 
        [IsSecondaryContactInformationComplete]
    )
    VALUES
    (
        @NewUserProfileCompletionID,
        @UserID,
        0,
        0,
        0,
        /* 
            We set the following columns to null because we don't know what the 
            responsible entity type is at this point, and values for these may not 
            be relevant.
        */
        NULL, 
        NULL,
        0
    );

END