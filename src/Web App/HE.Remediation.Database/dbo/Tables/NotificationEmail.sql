﻿CREATE TABLE [dbo].[NotificationEmail]
(	
	[Id]	UNIQUEIDENTIFIER NOT NULL,
	[Body]	NVARCHAR (150),
	[To]	NVARCHAR (150),
	[Sent]	DATETIME2 (7) NULL
);
