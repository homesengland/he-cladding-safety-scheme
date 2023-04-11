CREATE TABLE [dbo].[UserDetails] (
    [UserId]         UNIQUEIDENTIFIER NOT NULL,
    [Auth0UserId]    NVARCHAR (150)   NOT NULL,
    [FirstName]      NVARCHAR (150)   NULL,
    [LastName]       NVARCHAR (150)   NULL,
    [ContactNumber]  NVARCHAR (150)   NULL,
    [EmailAddress]   NVARCHAR (254)   NULL,
    [Created]        DATETIME2 (7)    NULL,
    [ResponsibleEntityTypeId] INT NULL,
    [LastLoginTime]  DATETIME2 (7)    NULL,
    [LoginCount]     INT              NULL,
    CONSTRAINT [PK_User_Details] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [UQ_User_Auth0UserId] UNIQUE NONCLUSTERED ([Auth0UserId] ASC),
    CONSTRAINT [FK_UserDetails_ApplicationResponsibleEntityType_ResponsibleEntityTypeId] 
        FOREIGN KEY ([ResponsibleEntityTypeId]) 
        REFERENCES [ApplicationResponsibleEntityType]([Id])
);

