CREATE TABLE [dbo].[ApplicationDeveloper]
(
	[Id]					UNIQUEIDENTIFIER NOT NULL,	
	[Name]					NVARCHAR (150),
	[AddressId]				UNIQUEIDENTIFIER,
	[StillInBusinessId]		INT,
	[DeveloperContacted]	BIT,
	CONSTRAINT [PK_ApplicationDeveloper] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_ApplicationDeveloper_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([Id]),
	CONSTRAINT [FK_ApplicationDeveloper_StillInBusinessId] FOREIGN KEY ([StillInBusinessId]) REFERENCES [dbo].[ApplicationDeveloperInBusinessType] ([Id])
);