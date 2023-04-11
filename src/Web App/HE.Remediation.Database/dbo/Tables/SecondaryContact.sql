CREATE TABLE [dbo].[SecondaryContact]
(
	[Id]			UNIQUEIDENTIFIER NOT NULL,	
	[UserId]        UNIQUEIDENTIFIER,	
	[CompanyId]		UNIQUEIDENTIFIER,
	[Name]			NVARCHAR (150),
	[ContactNumber]	NVARCHAR (150),
	[EmailAddress]	NVARCHAR (150)
    CONSTRAINT [PK_SecondaryContact] PRIMARY KEY CLUSTERED ([Id] ASC)
);
