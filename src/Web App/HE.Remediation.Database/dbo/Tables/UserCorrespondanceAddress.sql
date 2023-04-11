CREATE TABLE [dbo].[UserCorrespondanceAddress]
(	
	[Id]				UNIQUEIDENTIFIER NOT NULL,
	[UserId]			UNIQUEIDENTIFIER,
	[AddressId]			UNIQUEIDENTIFIER,
	CONSTRAINT [PK_UserCorrespondanceAddress] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_UserCorrespondanceAddress_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([Id])
)
