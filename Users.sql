-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (MAX) NOT NULL,
    [LastUpdatedBy] NVARCHAR (50)  NOT NULL,
    [LastUpdatedAt] DATETIME       NOT NULL,
    [UpdateVersion] ROWVERSION     NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO