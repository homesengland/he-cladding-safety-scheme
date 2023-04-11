CREATE TABLE [dbo].[ApplicationRepresentationEntityDetails]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[AddressId] UNIQUEIDENTIFIER NULL,
	[ResponsibleEntityTypeId] INT NULL,
	[CompanyName] NVARCHAR(254) NULL,
	[CompanyRegistration] NVARCHAR(254) NULL,
	[FirstName] NVARCHAR(254) NULL,
	[LastName] NVARCHAR(254) NULL,
	[EmailAddress] NVARCHAR(254) NULL,
	[ContactNumber] NVARCHAR(254) NULL,
	CONSTRAINT [PK_ApplicationRepresentationEntityDetails] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_ApplicationRepresentationEntityDetails_Address] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([Id]),
	CONSTRAINT [FK_ApplicationRepresentationEntityDetails_ResponsibleEntityType] FOREIGN KEY ([ResponsibleEntityTypeId]) REFERENCES [dbo].[ApplicationResponsibleEntityType] ([Id])
)
