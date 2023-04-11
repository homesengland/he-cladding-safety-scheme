CREATE TABLE [dbo].[WorkpackageWorksPlanning]
(
	[Id]									INT NOT NULL,
	[RequiresPlanningApproval]				BIT,
	[PlanningApprovalInPlacePriorToStart]	BIT,
	[FullPlansApproval]						BIT,
	[AllStatatoryApprovalsInPlace]			BIT
);
