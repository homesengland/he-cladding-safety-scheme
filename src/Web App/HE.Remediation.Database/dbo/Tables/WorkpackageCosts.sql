CREATE TABLE [dbo].[WorkpackageCosts]
(
	[Id]								UNIQUEIDENTIFIER NOT NULL,
	[RemovalOfCladding]					DECIMAL(19,5),
	[NewCladding]						DECIMAL(19,5),
	[OtherEligibleWorkToExternalWall]	DECIMAL(19,5),
	[InternalMitigationWorks]			DECIMAL(19,5),
	[NonEligibleWork]					DECIMAL(19,5),
	[MainContractorPreliminaries]		DECIMAL(19,5),
	[Access]							DECIMAL(19,5),
	[OverheadsAndProfit]				DECIMAL(19,5),
	[ContractorContingencies]			DECIMAL(19,5),
	[FeasibilityStage]					DECIMAL(19,5),
	[PostTenderStage]					DECIMAL(19,5),
	[IrrecoverableVat]					DECIMAL(19,5),
	[PropertyManager]					DECIMAL(19,5)
);
