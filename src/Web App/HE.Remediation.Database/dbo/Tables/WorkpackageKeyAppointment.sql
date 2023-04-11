CREATE TABLE [dbo].[WorkpackageKeyAppointment]
(
	[Id]								UNIQUEIDENTIFIER NOT NULL,		
	[RoleId]							UNIQUEIDENTIFIER,
	[WorkPackageId]						UNIQUEIDENTIFIER,
	[FullName]							NVARCHAR (150),
	[CompanyName]						NVARCHAR (150),
	[CompanyRegistrationNumber]			NVARCHAR (150),
	[EmailAddress]						NVARCHAR (150),
	[ContactNumber]						NVARCHAR (150),
	[ContractSigned]					BIT,
	[IndemnityInsuranceInPlace]			BIT,
	[InvolvedInOriginalCladdingInstall] BIT
);
