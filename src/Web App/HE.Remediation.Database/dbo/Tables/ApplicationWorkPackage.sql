CREATE TABLE [dbo].[ApplicationWorkPackage]
(
	[Id]					UNIQUEIDENTIFIER NOT NULL,	
	[ProfessionalId]		UNIQUEIDENTIFIER,
	[CostsScheduleId]		UNIQUEIDENTIFIER,
	[WorksPlanningId]		UNIQUEIDENTIFIER,
	[ProjectTimetableId]	UNIQUEIDENTIFIER,
	[DeclarationId]			UNIQUEIDENTIFIER
);
