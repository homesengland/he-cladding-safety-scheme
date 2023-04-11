CREATE PROCEDURE [dbo].[SetUserProfileCompletionStatusByUserId]
    @UserId UNIQUEIDENTIFIER,
    @IsContactInformationComplete BIT,
    @IsCorrespondenceAddressComplete BIT,
    @IsResponsibleEntityTypeSelectionComplete BIT,
    @IsCompanyDetailsComplete BIT,
    @IsCompanyAddressComplete BIT,
    @IsSecondaryContactInformationComplete BIT
AS
BEGIN

    UPDATE
        UPC
    SET
        UPC.[IsContactInformationComplete] = @IsContactInformationComplete,
        UPC.[IsCorrespondenceAddressComplete] = @IsCorrespondenceAddressComplete,
        UPC.[IsResponsibleEntityTypeSelectionComplete] = @IsResponsibleEntityTypeSelectionComplete,
        UPC.[IsCompanyDetailsComplete] = @IsCompanyDetailsComplete,
        UPC.[IsCompanyAddressComplete] = @IsCompanyAddressComplete,
        UPC.[IsSecondaryContactInformationComplete] = @IsSecondaryContactInformationComplete
    FROM
        [UserProfileCompletion] UPC
    WHERE
        UPC.[UserId] = @UserId

END