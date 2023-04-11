CREATE TABLE [dbo].[UserProfileCompletion]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [IsContactInformationComplete] BIT NOT NULL,
    [IsCorrespondenceAddressComplete] BIT NOT NULL,
    [IsResponsibleEntityTypeSelectionComplete] BIT NOT NULL, 
    [IsCompanyDetailsComplete] BIT NULL, 
    [IsCompanyAddressComplete] BIT NULL, 
    [IsSecondaryContactInformationComplete] BIT NOT NULL,
    
    CONSTRAINT [PK_UserProfileCompletion] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserProfileCompletion_UserDetails_UserId] FOREIGN KEY ([UserId]) REFERENCES [UserDetails]([UserId]),
    CONSTRAINT [UQ_UserProfileCompletion_UserId] UNIQUE ([UserId])
)
