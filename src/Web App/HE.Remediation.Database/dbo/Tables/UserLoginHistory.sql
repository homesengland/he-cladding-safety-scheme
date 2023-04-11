CREATE TABLE [dbo].[UserLoginHistory]
(
	[Id]				UNIQUEIDENTIFIER NOT NULL,
	[UserId]			UNIQUEIDENTIFIER NOT NULL,
	[LoginTime]			DATETIME2 (7) NULL,
	[UserAgent]			NVARCHAR (150),
	[IPAddress]			NVARCHAR (150),

	CONSTRAINT [PK_UserLoginHistory] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_UserLoginHistory_UserDetails_UserId] FOREIGN KEY ([UserId]) REFERENCES [UserDetails]([UserId])
)
