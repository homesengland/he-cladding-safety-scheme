CREATE TABLE [dbo].[ApplicationStateHistory]
(
	[Id]			UNIQUEIDENTIFIER NOT NULL,	
	[AppId]			UNIQUEIDENTIFIER,	
	[StatusId]		INT NOT NULL,
	[StateChanged]	DATETIME2 (7) NOT NULL
)
