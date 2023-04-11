CREATE TABLE [dbo].[CompanyDetails]
(	
	[Id]					UNIQUEIDENTIFIER NOT NULL,
	[Name]					NVARCHAR (150),
	[RegistrationNumber]	NVARCHAR (150),
	CONSTRAINT [PK_CompanyDetails] PRIMARY KEY CLUSTERED ([Id])
);
	