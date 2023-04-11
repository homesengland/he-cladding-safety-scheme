CREATE TABLE [dbo].[CompanyAddress]
(
	[AddressId] UNIQUEIDENTIFIER,
	[CompanyId] UNIQUEIDENTIFIER,
	CONSTRAINT [CompanyAddress_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[CompanyDetails] ([Id]),
	CONSTRAINT [CompanyAddress_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([Id])
)
