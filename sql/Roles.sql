-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (MAX) NOT NULL,
    [Description]   NVARCHAR (MAX) NOT NULL,
    [LastUpdatedBy] NVARCHAR (50)  NOT NULL,
    [LastUpdatedAt] DATETIME       NOT NULL,
    [UpdateVersion] ROWVERSION     NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
-- Creating primary key on [Id] in table 'Roles'

GO