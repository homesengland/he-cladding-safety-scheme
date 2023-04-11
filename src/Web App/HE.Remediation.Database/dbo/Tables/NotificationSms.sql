CREATE TABLE [dbo].[NotificationSms]
(
	[Id]		UNIQUEIDENTIFIER NOT NULL,
	[To]		NVARCHAR (150),
	[Content]	NVARCHAR (150),
	[Sent]		DATETIME2 (7) NULL
);
