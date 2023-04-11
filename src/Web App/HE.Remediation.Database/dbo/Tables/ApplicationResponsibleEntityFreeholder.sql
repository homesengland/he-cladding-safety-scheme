CREATE TABLE ApplicationResponsibleEntityFreeholder (
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[ResponsibleEntityTypeId] INT NULL,
	[CompanyName] [nvarchar](150) NULL,
	[CompanyRegistrationNumber] [nvarchar](150) NULL,
	[FirstName] [nvarchar](150) NULL,
	[LastName] [nvarchar](150) NULL,
	[EmailAddress] [nvarchar](150) NULL,
	[ContactNumber] [nvarchar](150) NULL,
	[AddressId] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [FK_ApplicationResponsibleEntityFreeholder_ResponsibleEntityTypeId] FOREIGN KEY([ResponsibleEntityTypeId])
REFERENCES [dbo].[ApplicationResponsibleEntityType] ([Id]),
	CONSTRAINT [FK_ApplicationResponsibleEntityFreeholder_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([Id])
)