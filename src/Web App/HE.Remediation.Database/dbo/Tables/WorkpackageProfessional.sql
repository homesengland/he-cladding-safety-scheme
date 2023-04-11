CREATE TABLE [dbo].[WorkpackageProfessional]
(
	[Id]						UNIQUEIDENTIFIER NOT NULL,	
	[FullName]					NVARCHAR (150),
	[CompanyName]				NVARCHAR (150),
	[CompanyRegistrationNumber]	NVARCHAR (150),
	[EmailAddress]				NVARCHAR (150),
	[ContactNumber]				NVARCHAR (150),
	[AddressId]					UNIQUEIDENTIFIER,
	[DateAppointed]				DATETIME2 (7),
	[ConfirmResponsibility]		BIT
);