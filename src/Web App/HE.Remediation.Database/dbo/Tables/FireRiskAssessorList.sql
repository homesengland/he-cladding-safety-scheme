CREATE TABLE [dbo].[FireRiskAssessorList] (
    [Id]             INT            NOT NULL,
    [CompanyName]    NVARCHAR (150) NOT NULL,
    [RegionsCovered] NVARCHAR (150) NOT NULL,
    [EmailAddress]   NVARCHAR (150) NOT NULL,
    [Telephone]      NVARCHAR (150) NOT NULL,
    CONSTRAINT [PK_FireRiskAssessorList] PRIMARY KEY CLUSTERED ([Id] ASC)
);



