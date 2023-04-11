CREATE TABLE [dbo].[WorkpackageCostsDescription]
(
	[Id]								UNIQUEIDENTIFIER NOT NULL,
	[RemovalOfCladding]					NVARCHAR (150),
	[NewCladding]						NVARCHAR (150),
	[OtherEligibleWorkToExternalWall]	NVARCHAR (150),
	[InternalMitigationWorks]			NVARCHAR (150),
	[NonEligibleWork]					NVARCHAR (150),
	[MainContractorPreliminaries]		NVARCHAR (150),
	[Access]							NVARCHAR (150),
	[OverheadsAndProfit]				NVARCHAR (150),
	[ContractorContingencies]			NVARCHAR (150),
	[FeasibilityStage]					NVARCHAR (150),
	[PostTenderStage]					NVARCHAR (150),
	[IrrecoverableVat]					NVARCHAR (150),
	[PropertyManager]					NVARCHAR (150)
);
