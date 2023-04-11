CREATE TABLE [dbo].[ApplicationResponsibleEntityFile]
(
	[FileId] UNIQUEIDENTIFIER NOT NULL,
	[ResponsibleEntityId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [FK_ApplicationResponsibleEntityFile_File] FOREIGN KEY ([FileId]) REFERENCES [dbo].[File] ([Id]),
	CONSTRAINT [FK_ApplicationResponsibleEntityFile_ResponsibleEntity] FOREIGN KEY ([ResponsibleEntityId]) REFERENCES [dbo].[ApplicationResponsibleEntity] ([Id])
)
