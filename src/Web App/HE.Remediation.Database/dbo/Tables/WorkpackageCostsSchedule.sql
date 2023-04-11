CREATE TABLE [dbo].[WorkpackageCostsSchedule]
(
	[Id]							UNIQUEIDENTIFIER NOT NULL,
	[ProofOfMoneySought]			BIT,
	[TotalProjectCostsId]			UNIQUEIDENTIFIER,
	[TotalCostsEligibleForGrantId]	UNIQUEIDENTIFIER,
	[CostsDescriptionId]			UNIQUEIDENTIFIER
);
