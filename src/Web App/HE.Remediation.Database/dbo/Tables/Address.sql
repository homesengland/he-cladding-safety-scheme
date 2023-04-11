CREATE TABLE [dbo].[Address]
(
	[Id]				UNIQUEIDENTIFIER NOT NULL,
	[NameNumber]		NVARCHAR (150) NOT NULL,
	[AddressLine1]		NVARCHAR (150) NOT NULL,
	[AddressLine2]		NVARCHAR (150),
	[City]				NVARCHAR (150) NOT NULL,
	[County]			NVARCHAR (150),
	[Postcode]			NVARCHAR (10) NOT NULL,
	[LocalAuthority]	NVARCHAR (150) NULL
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([Id])
);
