
CREATE TABLE [dbo].[WorkpackageProjectTimetable]
(
	[Id]								INT NOT NULL,
	[StartDateAgreed]					BIT,
	[StartDate]							DATETIME2 (7),
	[CladdingRemovalDateAgreed]			BIT,
	[CladdingRemovalDate]				DATETIME2 (7),
	[ExpectedDateForCompletionAgreed]	BIT,
	[ExpectedDateForCompletion]			DATETIME2 (7)
);
