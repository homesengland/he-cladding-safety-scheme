CREATE TABLE [dbo].[ApplicationDetails]
(
	[Id]						UNIQUEIDENTIFIER NOT NULL,
	[ReferenceNumber]			NVARCHAR(50) NOT NULL,
	[UserId]					UNIQUEIDENTIFIER,
	[CompanyId]					UNIQUEIDENTIFIER,
	[BuildingDetailsId]			UNIQUEIDENTIFIER,
	[ResponsibleEntityId]		UNIQUEIDENTIFIER,
	[AlternateFundingId]		UNIQUEIDENTIFIER,
	[BankDetailsId]				UNIQUEIDENTIFIER,
	[FireRiskAssessmentId]		UNIQUEIDENTIFIER,
	[LeaseHolderEngagementId]	UNIQUEIDENTIFIER,
	[ConfirmDeclaration]        BIT              CONSTRAINT [DF_ApplicationDetails_ConfirmDeclaration] DEFAULT ((0)) NOT NULL,
	[Submitted]					BIT,
	[StatusId]					INT NOT NULL DEFAULT 1,
	[StageId]					INT NOT NULL DEFAULT 1,
	[PreTenderSupportId]		UNIQUEIDENTIFIER,
	[WorkPackageId]				UNIQUEIDENTIFIER,
	[CreationDate]				DATETIME2 (7),
	CONSTRAINT [PK_ApplicationDetails] PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT [FK_ApplicationDetails_ApplicationAlternateFunding] FOREIGN KEY ([AlternateFundingId]) REFERENCES [dbo].[ApplicationAlternateFunding] ([Id]),
    CONSTRAINT [FK_ApplicationDetails_ApplicationFireRiskAssessment] FOREIGN KEY ([FireRiskAssessmentId]) REFERENCES [dbo].[ApplicationFireRiskAssessment] ([Id]),
	CONSTRAINT [FK_ApplicationDetails_ResponsibleEntity] FOREIGN KEY ([ResponsibleEntityId]) REFERENCES [dbo].[ApplicationResponsibleEntity] ([Id]),
	CONSTRAINT [FK_ApplicationDetails_BuildingDetails] FOREIGN KEY ([BuildingDetailsId]) REFERENCES [dbo].[ApplicationBuildingDetails] ([Id]),
    CONSTRAINT [FK_ApplicationDetails_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[CompanyDetails] ([Id]), 
    CONSTRAINT [FK_ApplicationDetails_UserDetails] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserDetails] ([UserId])
);




